namespace DesafioBtg.DataTransfer.Dados.Responses
{
    public class DadoServicoResponse
    {
        public string Name { get; set; }
        public string EmExecucao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataUltimaExecucao { get; set; }
        public DateTime DataEncerramento { get; set; }

        public DadoServicoResponse(){}
    }
}
