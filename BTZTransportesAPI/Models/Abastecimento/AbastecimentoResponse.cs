using BTZTransportesAPI.Models.Enums;

namespace BTZTransportesAPI.Models.Abastecimento
{
    public class AbastecimentoResponse
    {
        public int Id { get; set; }
        public string VeiculoNome { get; set; }
        public string MotoristaNome { get; set; }
        public DateTime Data { get; set; }
        public string TipoCombustivel { get; set; }
        public double QuantidadeAbastecida { get; set; }
        public double ValorTotal { get; set; }
    }
}
