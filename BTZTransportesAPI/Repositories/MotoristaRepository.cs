using BTZTransportesAPI.Models;
using BTZTransportesAPI.Models.Enums;
using BTZTransportesAPI.Repositories.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Http.Metadata;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;

namespace BTZTransportesAPI.Repositories
{
    public class MotoristaRepository : IMotoristaRepository
    {
        private readonly string _connectionString;
        public MotoristaRepository(IConfiguration configuration) => 
            _connectionString = configuration.GetConnectionString("DefautConnection");

      
        public IEnumerable<Motorista> GetMotoristas() 
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"SELECT * FROM Motoristas";
                var motoristas = db.Query<Motorista>(query);
                return motoristas;
            }

        }

        public Motorista GetMotoristasById(int motoristaId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"SELECT * FROM Motoristas WHERE Id = @Id";

                var Motorista = db.Query<Motorista>(query,new { id = motoristaId }).SingleOrDefault();

                return Motorista;
            }
        }

            public Motorista RegisterMotorista(Motorista Motorista)
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    db.Open();

                    var query = @"
                            INSERT INTO Motoristas (
                                nome, 
                                cpf,
                                NumeroCNH, 
                                categoriaCNH_id, 
                                data_nascimento, 
                                status) 
                            VALUES (
                                @Nome, 
                                @CPF, 
                                @NumeroCNH, 
                                @CategoriaCNH, 
                                @DataNascimento, 
                                @Status);
                            SELECT CAST(SCOPE_IDENTITY() as int)";


                    int motoristaId = db.Query<int>(query, Motorista).Single();
                    var registedMotorista = db.Query<Motorista>("SELECT * FROM Motoristas WHERE id = @id", new { id = motoristaId }).SingleOrDefault();

                    return registedMotorista;
                }
            }

            public Motorista EditMotorista(Motorista Motorista)
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    db.Open();

                    var query = @"
                    UPDATE Motoristas 
                    SET 
                        nome = @Nome, 
                        cpf = @CPF, 
                        NumeroCNH = @NumeroCNH, 
                        categoriaCNH_id = @CategoriaCNH, 
                        data_nascimento = @DataNascimento, 
                        status = @Status
                    WHERE id = @Id";

                    db.Execute(query, Motorista);

                    var updatedMotorista = db.Query<Motorista>("SELECT * FROM Motoristas WHERE id = @id", new { id = Motorista.Id }).SingleOrDefault();

                    return updatedMotorista;
                }
            }

            public Motorista DeleteMotorista(int motoristaId)
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    db.Open();

                    var query = "SELECT * FROM Motoristas WHERE id = @Id";
                    var motorista = db.Query<Motorista>(query, new { Id = motoristaId }).SingleOrDefault();

                    if (motorista != null)
                    {
                        var deleteQuery = "DELETE FROM Motoristas WHERE id = @Id";
                        db.Execute(deleteQuery, new { Id = motoristaId });
                    }

                    return motorista;
                }
            }
        }
}