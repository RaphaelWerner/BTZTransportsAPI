using BTZTransportesAPI.Models.Enums;

namespace BTZTransportesAPI.Models
{
    public class Veiculo
    {
        public int? Id { get; set; }
        public required string Nome { get; set; }
        public required string Placa { get; set; }
        public required Combustivel TipoCombustivel { get; set; }
        public required string Fabricante { get; set; }
        public required int AnoFabricacao { get; set; }
        public required double CapacidadeTanque { get; set; }
        public string? Observacoes { get; set; }
    }
}
