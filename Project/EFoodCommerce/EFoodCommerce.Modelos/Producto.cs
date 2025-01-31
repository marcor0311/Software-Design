using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace EFoodCommerce.Modelos
{
    public class Producto : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [StringLength(64)]
        public string Nombre { get; set; } = null!;

        [Required]
        public int CodigoLineaComida { get; set; }

        [ForeignKey("CodigoLineaComida")]
        public LineaComida? LineaComida { get; set; } // Navigation property

        public string? Contenido { get; set; }

        public string? RutaImagen { get; set; }
    }
}
