using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFoodCommerce.Modelos
{
    public class EstadoPedidoValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success; // Allow null values

            var estado = value.ToString()?.ToLower();
            if (estado == "en concurso" || estado == "cancelado" || estado == "procesado")
                return ValidationResult.Success;

            return new ValidationResult("El estado del pedido debe ser 'en concurso', 'cancelado', o 'procesado'.");
        }
    }

    public class Pedido : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El monto debe ser mayor o igual a 0.")]
        public int Monto { get; set; }

        [Required(ErrorMessage = "La fecha y hora del pedido son obligatorias.")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "El estado del pedido es obligatorio.")]
        [MaxLength(64)]
        [EstadoPedidoValido(ErrorMessage = "El estado del pedido debe ser 'en concurso', 'cancelado', o 'procesado'.")]
        public string Estado { get; set; } = null!;

        [Required(ErrorMessage = "El código de descuento es obligatorio.")]
        //[StringLength(50)]
        public int CodigoDescuento { get; set; }

        [ForeignKey("CodigoDescuento")]
        public TiqueteDescuento? TiqueteDescuento { get; set; }
    }
}
