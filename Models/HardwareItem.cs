namespace AberturaFechamentoBlazor.Models;

public class HardwareItem
{
    public string CI { get; set; } = "";
    public string SerialNumber { get; set; } = "";
    public string Floor { get; set; } = "";
    public string Room { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string AssetTag { get; set; } = "";

    public string Model => DisplayName;
}
