namespace DesafioBtg.Ioc.Abstracoes;

public class DadosServico
{
    public string? Name { get; set; }
    public bool EmExecucao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataUltimaExecucao { get; set; }
    public DateTime DataEncerramento { get; set; }
}
