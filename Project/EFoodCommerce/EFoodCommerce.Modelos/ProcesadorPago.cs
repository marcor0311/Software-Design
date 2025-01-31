using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EFoodCommerce.Modelos
{
    public class TipoPagoValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success; // Allow null values

            var tipo = value.ToString()?.ToLower();
            if (tipo == "cheque" || tipo == "efectivo" || tipo == "debito/credito")
                return ValidationResult.Success;

            return new ValidationResult("El tipo de pago debe ser 'cheque', 'efectivo' o 'debito/credito'.");
        }
    }

    public class ProcesadorPago : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [MaxLength(64)]
        [Remote(action: "ValidarNombre", controller: "ProcesadorPago", areaName: "Admin", AdditionalFields = "Codigo")]
        public string NombreProcesador { get; set; } = null!;

        [Required]
        [MaxLength(512)]
        [TipoPagoValido(ErrorMessage = "El tipo de pago debe ser 'cheque', 'efectivo' o 'debito/credito'.")]
        [Remote(action: "ValidarTipo", controller: "ProcesadorPago", areaName: "Admin", AdditionalFields = "Codigo")]
        public string Tipo { get; set; } = null!;

        [Required]
        [StringLength(64)]
        public string NombreOpcionPago { get; set; } = null!;

        [Required]
        [Remote(action: "ValidarEstado", controller: "ProcesadorPago", areaName: "Admin", AdditionalFields = "Codigo,Tipo")]
        public bool Estado { get; set; }

        [Required]
        public bool Verificacion { get; set; }

        [Required]
        [StringLength(64)]
        public string Metodo { get; set; } = null!;
    }
}
