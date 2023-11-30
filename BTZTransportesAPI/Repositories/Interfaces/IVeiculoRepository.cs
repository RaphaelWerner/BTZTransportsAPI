using BTZTransportesAPI.Models;

namespace BTZTransportesAPI.Repositories.Interfaces
{
    public interface IVeiculoRepository
    {
        public IEnumerable<Veiculo> GetVeiculos();
        public Veiculo GetVeiculoById(int VeiculoId);
        public Veiculo RegisterVeiculo(Veiculo Veiculo);
        public Veiculo EditVeiculo(Veiculo Veiculo);
        public Veiculo DeleteVeiculo(int VeiculoId);
    }
}
