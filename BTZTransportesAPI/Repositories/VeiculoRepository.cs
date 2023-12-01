using BTZTransportesAPI.Models;
using BTZTransportesAPI.Repositories.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace BTZTransportesAPI.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly string _connectionString;
        public VeiculoRepository(IConfiguration configuration) =>
            _connectionString = configuration.GetConnectionString("DefautConnection");
        public IEnumerable<Veiculo> GetVeiculos()
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"SELECT
                                id as Id,
                                placa as Nome,
                                nome as Placa,
                                tipo_combustivel_id as TipoCombustivel,
                                fabricante as Fabricante,
                                ano_fabricacao as AnoFabricacao,
                                capacidade_tanque as CapacidadeTanque,
                                observacoes as Observacoes
                            FROM Veiculos";
                var result = db.Query<Veiculo>(query);
                return result;
            }

        }

        public Veiculo GetVeiculoById(int veiculoId)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"SELECT id as Id,
                                nome = @Nome, 
                                placa = @Placa, 
                                tipo_combustivel_id as TipoCombustivel,
                                fabricante as Fabricante,
                                ano_fabricacao as AnoFabricacao,
                                capacidade_tanque as CapacidadeTanque,
                                observacoes as Observacoes 
                            FROM Motoristas WHERE Id = @Id";

                var result = db.Query<Veiculo>(query, new { id = veiculoId }).Single();

                return result;
            }

        }

        public Veiculo RegisterVeiculo(Veiculo Veiculo)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"
                        INSERT INTO Veiculos (
                            nome, 
                            placa,
                            tipo_combustivel_id, 
                            fabricante, 
                            ano_fabricacao, 
                            capacidade_tanque,
                            observacoes) 
                        VALUES (
                            @Nome,
                            @Placa,
                            @TipoCombustivel,
                            @Fabricante,
                            @AnoFabricacao,
                            @CapacidadeTanque,
                            @Observacoes);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                int veiculoId = db.Query<int>(query, Veiculo).Single();
                var result = db.Query<Veiculo>(@"SELECT id as Id,
                                placa as Nome,
                                nome as Placa,
                                tipo_combustivel_id as TipoCombustivel,
                                fabricante as Fabricante,
                                ano_fabricacao as AnoFabricacao,
                                capacidade_tanque as CapacidadeTanque,
                                observacoes as Observacoes 
                            FROM Veiculos WHERE Id = @Id", 
                            new { id = veiculoId }).Single();

                return result;

            }
        }

        public Veiculo EditVeiculo(Veiculo Veiculo)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"
                UPDATE Veiculos 
                SET 
                    nome = @Nome, 
                    placa = @Placa, 
                    tipo_combustivel_id = @TipoCombustivel, 
                    fabricante = @Fabricante, 
                    ano_fabricacao = @AnoFabricacao, 
                    capacidade_tanque = @CapacidadeTanque,
                    observacoes = @Observacoes
                WHERE id = @Id";

                db.Execute(query, Veiculo);

                var updatedVeiculo = db.Query<Veiculo>(@"
                SELECT 
                    id as Id,
                    nome as Nome,
                    placa as Placa,
                    tipo_combustivel_id as TipoCombustivel,
                    fabricante as Fabricante,
                    ano_fabricacao as AnoFabricacao,
                    capacidade_tanque as CapacidadeTanque,
                    observacoes as Observacoes
                FROM Veiculos WHERE id = @Id",
                new { id = Veiculo.Id }).SingleOrDefault();

                return updatedVeiculo;
            }
        }


        public Veiculo DeleteVeiculo(int veiculoId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = "SELECT * FROM Veiculos WHERE id = @Id";
                var veiculo = db.Query<Veiculo>(query, new { Id = veiculoId }).SingleOrDefault();

                if (veiculo != null)
                {
                    var deleteQuery = "DELETE FROM Veiculos WHERE id = @Id";
                    db.Execute(deleteQuery, new { Id = veiculoId });
                }

                return veiculo;
            }
        }
    }
}
