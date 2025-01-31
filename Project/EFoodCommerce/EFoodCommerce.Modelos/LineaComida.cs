using System.ComponentModel.DataAnnotations;

namespace EFoodCommerce.Modelos
{
    public class LineaComida : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [StringLength(64)]
        public string Nombre { get; set; } = null!;
    }
}
