using BTZTransportesAPI.Models;
using BTZTransportesAPI.Services;

namespace BTZTransportesAPI.Repositories.Interfaces
{
    public interface IAbastecimentoRepository
    {
        public Abastecimento RegisterAbastecimento(Abastecimento abastecimento);
        public Abastecimento GetAbastecimentoById(int id);
        public IEnumerable<Abastecimento> GetAbastecimentos();
    }
}
