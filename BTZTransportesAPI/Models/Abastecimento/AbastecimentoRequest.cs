using BTZTransportesAPI.Models.Enums;
using BTZTransportesAPI.Repositories;
using BTZTransportesAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.ComponentModel.DataAnnotations;

namespace BTZTransportesAPI.Models.Abastecimento
{
    public class AbastecimentoRequest
    {
        public required int VeiculoId { get; set; }
        public required int MotoristaId { get; set; }
        public DateTime Data { get; set; }

        [CombustivelAdequado(ErrorMessage = "Combustível inadequado para o veículo.")]
        public Combustivel TipoCombustivel { get; set; }

        [MenorOuIgualTamanhoTanque(ErrorMessage = "Quantidade maior que a capacidade do tanque.")]
        public double QuantidadeAbastecida { get; set; }
        public double ValorTotal { get; set; }


        private class CombustivelAdequado : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var abastecimento = (AbastecimentoRequest)validationContext.ObjectInstance;

                var serviceProvider = validationContext.GetService(typeof(IServiceProvider)) as IServiceProvider;
                var veiculoRepository = serviceProvider.GetService(typeof(IVeiculoRepository)) as IVeiculoRepository;

                var veiculo = veiculoRepository.GetVeiculoById(abastecimento.VeiculoId);

                if (abastecimento.TipoCombustivel != veiculo.TipoCombustivel)
                    return new ValidationResult(ErrorMessage);

                return ValidationResult.Success;

            }
        }

        private class MenorOuIgualTamanhoTanque : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var abastecimento = (AbastecimentoRequest)validationContext.ObjectInstance;

                var serviceProvider = validationContext.GetService(typeof(IServiceProvider)) as IServiceProvider;
                var veiculoRepository = serviceProvider.GetService(typeof(IVeiculoRepository)) as IVeiculoRepository;

                var veiculo = veiculoRepository.GetVeiculoById(abastecimento.VeiculoId);

                if (abastecimento.QuantidadeAbastecida > veiculo.CapacidadeTanque)
                    return new ValidationResult(ErrorMessage);

                return ValidationResult.Success;

            }
        }

    }
}
