using System.Text;
using AberturaFechamentoBlazor.Models;

namespace AberturaFechamentoBlazor.Services;

public class HardwareService
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<HardwareService> _logger;
    private List<HardwareItem> _hardwareList = new();
    private Dictionary<string, HardwareItem> _ciLookup = new(StringComparer.OrdinalIgnoreCase);
    private bool _isLoaded = false;
    private readonly object _lock = new();

    public HardwareService(IWebHostEnvironment env, ILogger<HardwareService> logger)
    {
        _env = env;
        _logger = logger;
    }

    public void EnsureLoaded()
    {
        if (_isLoaded) return;

        lock (_lock)
        {
            if (_isLoaded) return;

            var filePath = Path.Combine(_env.ContentRootPath, "alm_hardware.csv");
            if (!File.Exists(filePath))
            {
                _logger.LogWarning("Arquivo alm_hardware.csv não encontrado no caminho: {Path}", filePath);
                return;
            }

            try
            {
                var list = new List<HardwareItem>();
                var lookup = new Dictionary<string, HardwareItem>(StringComparer.OrdinalIgnoreCase);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var encoding = Encoding.GetEncoding("ISO-8859-1");

                using var stream = File.OpenRead(filePath);
                using var reader = new StreamReader(stream, encoding);

                var headerLine = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(headerLine)) return;

                var headers = ParseCsvLine(headerLine);
                int ciIndex = headers.FindIndex(h => h.Equals("ci", StringComparison.OrdinalIgnoreCase));
                int serialIndex = headers.FindIndex(h => h.Equals("serial_number", StringComparison.OrdinalIgnoreCase));
                int floorIndex = headers.FindIndex(h => h.Equals("u_floor", StringComparison.OrdinalIgnoreCase));
                int roomIndex = headers.FindIndex(h => h.Equals("u_room", StringComparison.OrdinalIgnoreCase));
                int displayNameIndex = headers.FindIndex(h => h.Equals("display_name", StringComparison.OrdinalIgnoreCase));
                int assetTagIndex = headers.FindIndex(h => h.Equals("asset_tag", StringComparison.OrdinalIgnoreCase));

                while (reader.ReadLine() is { } line)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var cols = ParseCsvLine(line);
                    if (cols.Count == 0) continue;

                    var item = new HardwareItem
                    {
                        CI = ciIndex >= 0 && ciIndex < cols.Count ? cols[ciIndex].Trim() : "",
                        SerialNumber = serialIndex >= 0 && serialIndex < cols.Count ? cols[serialIndex].Trim() : "",
                        Floor = floorIndex >= 0 && floorIndex < cols.Count ? cols[floorIndex].Trim() : "",
                        Room = roomIndex >= 0 && roomIndex < cols.Count ? cols[roomIndex].Trim() : "",
                        DisplayName = displayNameIndex >= 0 && displayNameIndex < cols.Count ? cols[displayNameIndex].Trim() : "",
                        AssetTag = assetTagIndex >= 0 && assetTagIndex < cols.Count ? cols[assetTagIndex].Trim() : ""
                    };

                    list.Add(item);

                    if (!string.IsNullOrWhiteSpace(item.CI) && !lookup.ContainsKey(item.CI))
                    {
                        lookup[item.CI] = item;
                    }
                }

                _hardwareList = list;
                _ciLookup = lookup;
                _isLoaded = true;
                _logger.LogInformation("alm_hardware.csv carregado com sucesso: {Count} registros.", list.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar alm_hardware.csv");
            }
        }
    }

    public HardwareItem? BuscarPorCI(string ci)
    {
        EnsureLoaded();
        if (string.IsNullOrWhiteSpace(ci)) return null;

        var cleanCi = ci.Trim();
        if (_ciLookup.TryGetValue(cleanCi, out var match))
        {
            return match;
        }

        return _hardwareList.FirstOrDefault(h => string.Equals(h.CI, cleanCi, StringComparison.OrdinalIgnoreCase));
    }

    public HardwareItem? BuscarPorTermo(string termo)
    {
        EnsureLoaded();
        if (string.IsNullOrWhiteSpace(termo)) return null;

        var t = termo.Trim();
        if (_ciLookup.TryGetValue(t, out var match))
        {
            return match;
        }

        return _hardwareList.FirstOrDefault(h =>
            string.Equals(h.CI, t, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(h.SerialNumber, t, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(h.AssetTag, t, StringComparison.OrdinalIgnoreCase));
    }

    public List<HardwareItem> Filtrar(string? termo, int maxResultados = 100)
    {
        EnsureLoaded();

        if (string.IsNullOrWhiteSpace(termo))
        {
            return _hardwareList.Take(maxResultados).ToList();
        }

        var t = termo.Trim();
        return _hardwareList
            .Where(h => (h.SerialNumber != null && h.SerialNumber.Contains(t, StringComparison.OrdinalIgnoreCase)) ||
                        (h.AssetTag != null && h.AssetTag.Contains(t, StringComparison.OrdinalIgnoreCase)) ||
                        (h.CI != null && h.CI.Contains(t, StringComparison.OrdinalIgnoreCase)) ||
                        (h.DisplayName != null && h.DisplayName.Contains(t, StringComparison.OrdinalIgnoreCase)))
            .Take(maxResultados)
            .ToList();
    }

    public int TotalRegistros
    {
        get
        {
            EnsureLoaded();
            return _hardwareList.Count;
        }
    }

    private static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        var sb = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '\"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                {
                    sb.Append('\"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(sb.ToString().Trim('\"'));
                sb.Clear();
            }
            else
            {
                sb.Append(c);
            }
        }

        result.Add(sb.ToString().Trim('\"'));
        return result;
    }
}
