using System.ComponentModel.DataAnnotations;

namespace EFoodCommerce.Modelos
{
    public class Error : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        [MaxLength(512)]
        public string Mensaje { get; set; } = null!;
    }
}
