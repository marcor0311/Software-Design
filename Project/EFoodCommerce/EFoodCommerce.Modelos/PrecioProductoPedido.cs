using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFoodCommerce.Modelos
{
    public class PrecioProductoPedido : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public int CodigoPrecioProducto { get; set; }

        [ForeignKey("CodigoPrecioProducto")]
        public PrecioProducto? PrecioProducto { get; set; }

        [Required]
        public int CodigoPedido { get; set; }

        [ForeignKey("CodigoPedido")]
        public Pedido? Pedido { get; set; } // Navigation property

        [Required(ErrorMessage = "La cantidad debe ser mayor o igual a 1.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 1.")]
        public int Cantidad { get; set; }
    }
}
