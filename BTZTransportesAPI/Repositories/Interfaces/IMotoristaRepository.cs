using BTZTransportesAPI.Models;

namespace BTZTransportesAPI.Repositories.Interfaces
{
    public interface IMotoristaRepository
    {
        public IEnumerable<Motorista> GetMotoristas();
        public Motorista GetMotoristasById(int motoristaId);
        public Motorista RegisterMotorista(Motorista motorista);
        public Motorista EditMotorista(Motorista motorista);
        public Motorista DeleteMotorista(int motoristaId);
    }
}
