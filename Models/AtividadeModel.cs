namespace AberturaFechamentoBlazor.Models;

public class AtividadeModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Data { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
    public string DescricaoFalha { get; set; } = "";
    public string AcaoCorretiva { get; set; } = "";
    public string Etiqueta { get; set; } = "";
    public string Serial { get; set; } = "";
    public string Galpao { get; set; } = "";
    public string RoomSetor { get; set; } = "";
    public string Solicitante { get; set; } = "";
    public string Chamado { get; set; } = "";
    public string Celular { get; set; } = "(31)9 95426967";

    // Substituição
    public bool EquipamentoSubstituido { get; set; } = false;

    // Instalado
    public string EqSerial { get; set; } = "";
    public string EqIp { get; set; } = "";
    public string EqItemConfig { get; set; } = "";
    public string EqModelo { get; set; } = "";
    public string EqFloor { get; set; } = "";
    public string EqRoom { get; set; } = "";
    public string EqAplicacao { get; set; } = "";
    public string EqEstado { get; set; } = "Em uso";
    public string EqSubstatus { get; set; } = "Disponível";
    public string EqFuncao { get; set; } = "Compartilhado";

    // Recolhido
    public string RecModelo { get; set; } = "";
    public string RecFloor { get; set; } = "Galpão 01";
    public string RecRoom { get; set; } = "Sala 12 (Laboratório Atos)";
    public string RecEstado { get; set; } = "Em estoque";
    public string RecSubstatus { get; set; } = "Recuperação pendente";

    public string GerarTextoResumo()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ABERTURA");
        sb.AppendLine($"DESCRIÇÃO: {DescricaoFalha}");
        sb.AppendLine($"SETOR: {RoomSetor}");
        sb.AppendLine($"GALPÃO: {Galpao}");
        sb.AppendLine($"CELULAR: {Celular}");
        sb.AppendLine($"ETIQUETA PATRIMONIAL: {Etiqueta}");
        sb.AppendLine($"SERIAL NUMBER: {Serial}");
        sb.AppendLine($"SOLICITANTE: {Solicitante}");
        sb.AppendLine();
        sb.AppendLine("ENCERRAMENTO");
        sb.AppendLine($"FALHA: {DescricaoFalha}");
        sb.AppendLine($"AÇÃO CORRETIVA: {AcaoCorretiva}");
        sb.AppendLine($"LOCALIZAÇÃO: {Galpao} - {RoomSetor}");
        sb.AppendLine($"ETIQUETA PATRIMONIAL: {Etiqueta}");
        sb.AppendLine($"SERIAL NUMBER: {Serial}");

        if (EquipamentoSubstituido)
        {
            sb.AppendLine();
            sb.AppendLine("Equipamento Recolhido");
            sb.AppendLine($"SERIAL: {Serial}");
            sb.AppendLine($"ITEM DE CONFIGURAÇÃO: {Etiqueta}");
            sb.AppendLine($"ETIQUETA: {Etiqueta}");
            sb.AppendLine($"MODELO: {RecModelo}");
            sb.AppendLine($"FLOOR: {RecFloor}");
            sb.AppendLine($"ROOM: {RecRoom}");
            sb.AppendLine("IP: ");
            sb.AppendLine($"ESTADO: {RecEstado}");
            sb.AppendLine($"SUBESTADO: {RecSubstatus}");
            sb.AppendLine("FUNÇÃO: ");
            sb.AppendLine();
            sb.AppendLine("Equipamento Instalado");
            sb.AppendLine($"SERIAL: {EqSerial}");
            sb.AppendLine($"ITEM DE CONFIGURAÇÃO: {EqItemConfig}");
            sb.AppendLine($"ETIQUETA: {EqItemConfig}");
            sb.AppendLine($"MODELO: {EqModelo}");
            sb.AppendLine($"FLOOR: {EqFloor}");
            sb.AppendLine($"ROOM: {EqRoom}");
            sb.AppendLine($"IP: {EqIp}");
            sb.AppendLine($"ESTADO: {EqEstado}");
            sb.AppendLine($"SUBESTADO: {EqSubstatus}");
            sb.AppendLine($"FUNÇÃO: {EqFuncao}");
            sb.AppendLine($"APLICAÇÃO: {EqAplicacao}");
        }

        return sb.ToString();
    }

    public string GerarTextoRegistro()
    {
        return $"DESCRIÇÃO DA FALHA: {DescricaoFalha}\n" +
               $"AÇÃO CORRETIVA: {AcaoCorretiva}\n" +
               $"SETOR: {RoomSetor}\n" +
               $"GALPÃO / COLUNA: {Galpao}\n" +
               $"CELULAR: {Celular}\n" +
               $"ETIQUETA PATRIMONIAL: {Etiqueta}\n" +
               $"SERIAL NUMBER: {Serial}\n" +
               $"DATA: {Data}\n" +
               $"CHAMADO: {Chamado}";
    }
}
