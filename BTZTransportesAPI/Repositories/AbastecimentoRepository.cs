﻿using BTZTransportesAPI.Models;
using BTZTransportesAPI.Repositories.Interfaces;
using BTZTransportesAPI.Services;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BTZTransportesAPI.Repositories
{
    public class AbastecimentoRepository: IAbastecimentoRepository
    {
        private readonly string _connectionString;
        public AbastecimentoRepository(IConfiguration configuration) =>
            _connectionString = configuration.GetConnectionString("DefautConnection");


        public IEnumerable<Abastecimento> GetAbastecimentos()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"SELECT * FROM Abastecimentos";
                var Abastecimentos = db.Query<Abastecimento>(query);
                return Abastecimentos;
            }
        }

        public Abastecimento GetAbastecimentoById(int abastecimentoId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"SELECT * FROM Abastecimentos WHERE id = @id";

                var Abastecimento = db.Query<Abastecimento>(query, new { id = abastecimentoId }).Single();

                return Abastecimento;
            }
        }
        public Abastecimento RegisterAbastecimento(Abastecimento abastecimento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"SELECT preco FROM Combustiveis WHERE id = @id";
                var precoCombustivel = db.Query<int>(query, new { id = abastecimento.TipoCombustivel }).Single();

                abastecimento.ValorTotal = new AbastecimentoService().CalcularPrecoAbastecimento(precoCombustivel, abastecimento.QuantidadeAbastecida);

                query = @"
                        INSERT INTO Abastecimento (
                            veiculo_id, 
                            motorista_id,
                            NumeroCNH, 
                            data_abastecimento, 
                            tipo_combustivel_id, 
                            quantidade_abastecida,
                            preco_total) 
                        VALUES (
                            @VeiculoId, 
                            @MotoristaId, 
                            @Data, 
                            @TipoCombustivel, 
                            @QuantidadeAbastecida, 
                            @ValorTotal);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                var parametros = new
                {
                    abastecimento.VeiculoId,
                    abastecimento.MotoristaId,
                    abastecimento.Data,
                    abastecimento.TipoCombustivel,
                    abastecimento.QuantidadeAbastecida,
                    abastecimento.ValorTotal
            };

                int abastecimentoId = db.Query<int>(query, parametros).Single();
                var result = db.Query<Abastecimento>("SELECT * FROM Abastecimentos WHERE id = @id", new { id = abastecimentoId }).Single();


                return result;
            }

        }

       


        
    }
}