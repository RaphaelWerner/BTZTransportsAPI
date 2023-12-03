using BTZTransportesAPI.Repositories.Interfaces;
using BTZTransportesAPI.Services;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BTZTransportesAPI.Models.Abastecimento;

namespace BTZTransportesAPI.Repositories
{
    public class AbastecimentoRepository: IAbastecimentoRepository
    {
        private readonly string _connectionString;
        public AbastecimentoRepository(IConfiguration configuration) =>
            _connectionString = configuration.GetConnectionString("DefautConnection");


        public IEnumerable<AbastecimentoResponse> GetAbastecimentos()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"SELECT 
                                A.id as Id,
                                V.nome as VeiculoNome, 
                                M.nome as MotoristaNome, 
                                data_abastecimento as Data, 
                                C.tipo as TipoCombustivel, 
                                quantidade_abastecida as QuantidadeAbastecida, 
                                valor_total as ValorTotal
                            from Abastecimentos A
                            inner join Motoristas M on M.id = A.motorista_id
                            inner join Veiculos V on V.id = A.veiculo_id
                            inner join Combustiveis C on C.id = A.tipo_combustivel_id";
                var Abastecimentos = db.Query<AbastecimentoResponse>(query);
                return Abastecimentos;
            }
        }

        public AbastecimentoResponse GetAbastecimentoById(int abastecimentoId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"SELECT SELECT id as Id
                                veiculo_id as VeiculoId
                                motorista_id as MotoristaId
                                data_abastecimento as Data
                                tipo_combustivel_id as TipoCombustivel
                                quantidade_abastecida as QuantidadeAbastecida
                                valor_total as ValorTotal
                            FROM Abastecimentos WHERE id = @id";

                var Abastecimento = db.Query<AbastecimentoResponse>(query, new { id = abastecimentoId }).Single();

                return Abastecimento;
            }
        }
        public AbastecimentoResponse RegisterAbastecimento(AbastecimentoRequest abastecimento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                var query = @"SELECT preco FROM Combustiveis WHERE id = @id";
                var precoCombustivel = db.Query<double>(query, new { id = abastecimento.TipoCombustivel }).Single();

                abastecimento.ValorTotal = new AbastecimentoService().CalcularPrecoAbastecimento(precoCombustivel, abastecimento.QuantidadeAbastecida);

                query = @"
                        INSERT INTO Abastecimentos (
                            veiculo_id, 
                            motorista_id,
                            data_abastecimento, 
                            tipo_combustivel_id, 
                            quantidade_abastecida,
                            valor_total) 
                        VALUES (
                            @VeiculoId, 
                            @MotoristaId, 
                            @Data, 
                            @TipoCombustivel, 
                            @QuantidadeAbastecida, 
                            @ValorTotal);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                int abastecimentoId = db.Query<int>(query, abastecimento).Single();

                query = @"SELECT 
                            A.id as Id,
                            V.nome as VeiculoNome, 
                            M.nome as MotoristaNome, 
                            data_abastecimento as Data, 
                            C.tipo as TipoCombustivel, 
                            quantidade_abastecida as QuantidadeAbastecida, 
                            valor_total as ValorTotal
                        from Abastecimentos A
                        inner join Motoristas M on M.id = A.motorista_id
                        inner join Veiculos V on V.id = A.veiculo_id
                        inner join Combustiveis C on C.id = A.tipo_combustivel_id";
                var result = db.Query<AbastecimentoResponse>(query, new { id = abastecimentoId }).Single();


                return result;
            }

        }

       


        
    }
}
