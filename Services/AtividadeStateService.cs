using AberturaFechamentoBlazor.Models;

namespace AberturaFechamentoBlazor.Services;

public class AtividadeStateService
{
    public List<AtividadeModel> Atividades { get; } = new();
    public AtividadeModel FormAtual { get; set; } = new();
    public AtividadeModel? UltimaAtividadeSalva => Atividades.Count > 0 ? Atividades[^1] : null;

    public event Action? OnChange;

    public void NotifyStateChanged() => OnChange?.Invoke();

    public void AdicionarAtividade(AtividadeModel atividade)
    {
        Atividades.Add(atividade);
        NotifyStateChanged();
    }

    public void RemoverAtividade(AtividadeModel atividade)
    {
        Atividades.Remove(atividade);
        NotifyStateChanged();
    }

    public void RemoverAtividadeNoIndice(int index)
    {
        if (index >= 0 && index < Atividades.Count)
        {
            Atividades.RemoveAt(index);
            NotifyStateChanged();
        }
    }

    public void LimparTodas()
    {
        Atividades.Clear();
        NotifyStateChanged();
    }
}
