using BTZTransportesAPI.Models.Abastecimento;
using BTZTransportesAPI.Services;

namespace BTZTransportesAPI.Repositories.Interfaces
{
    public interface IAbastecimentoRepository
    {
        public AbastecimentoResponse RegisterAbastecimento(AbastecimentoRequest abastecimento);
        public AbastecimentoResponse GetAbastecimentoById(int id);
        public IEnumerable<AbastecimentoResponse> GetAbastecimentos();
    }
}
