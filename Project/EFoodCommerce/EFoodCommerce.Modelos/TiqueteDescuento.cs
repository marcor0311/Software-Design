using System.ComponentModel.DataAnnotations;

namespace EFoodCommerce.Modelos
{
    public class TiqueteDescuento : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [StringLength(64)]
        public string StringCodigo { get; set; } = null!;

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        [Range(1, 100, ErrorMessage = "El descuento debe estar entre 1 y 100.")]
        public int Descuento { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Los disponibles deben ser igual o mayor a 0.")]
        public int Disponibles { get; set; }
    }
}
