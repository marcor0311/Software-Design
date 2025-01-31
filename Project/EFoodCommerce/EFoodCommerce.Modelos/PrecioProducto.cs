using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFoodCommerce.Modelos
{
    public class PrecioProducto : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public int CodigoProducto { get; set; }

        [ForeignKey("CodigoProducto")]
        public Producto? Producto { get; set; }

        [Required]
        public int CodigoTipoPrecio { get; set; }

        [ForeignKey("CodigoTipoPrecio")]
        public TipoPrecio? TipoPrecio { get; set; }

        [Required]
        public int Precio { get; set; }
    }
}
