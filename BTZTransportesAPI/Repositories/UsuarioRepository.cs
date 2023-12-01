using BTZTransportesAPI.Models;
using BTZTransportesAPI.Repositories.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace BTZTransportesAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;
        public UsuarioRepository(IConfiguration configuration) =>
            _connectionString = configuration.GetConnectionString("DefautConnection");
        public bool UsuarioESenhaCorretos(string usuario, string senha)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var query = @"SELECT * FROM Usuarios WHERE usuario = @usuario and senha = @senha";

                var id = db.QueryFirstOrDefault<int?>(query, new { usuario, senha });

                if (id != null)
                    return true;

                return false;
            }

        }
    }
}
