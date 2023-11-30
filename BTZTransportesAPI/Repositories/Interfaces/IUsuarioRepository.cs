namespace BTZTransportesAPI.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        public bool UsuarioESenhaCorretos(string username, string password);
    }
}
