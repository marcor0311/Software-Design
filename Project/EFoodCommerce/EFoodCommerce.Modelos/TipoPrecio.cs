using System.ComponentModel.DataAnnotations;

namespace EFoodCommerce.Modelos
{
    public class TipoPrecio : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [StringLength(64)]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(128)]
        public string Descripcion { get; set; } = null!;
    }
}
