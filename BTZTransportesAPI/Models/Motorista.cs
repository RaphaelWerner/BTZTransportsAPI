using BTZTransportesAPI.Models.Enums;
using BTZTransportesAPI.Repositories;
using System.ComponentModel.DataAnnotations;

namespace BTZTransportesAPI.Models
{
    public class Motorista
    {
        public int? Id { get; set; }
        public required string Nome { get; set; }

        [CPFValido(ErrorMessage = "CPF Inválido")]
        public required string CPF { get; set; }
        public required string NumeroCNH { get; set; }
        public required CategoriaCNH CategoriaCNH { get; set; }
        public required DateTime DataNascimento { get; set; }
        public required bool Status { get; set; }


        private class CPFValido : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value.ToString().Length != 11)
                    return new ValidationResult(ErrorMessage);

                return ValidationResult.Success;

            }
        }

    }
}
