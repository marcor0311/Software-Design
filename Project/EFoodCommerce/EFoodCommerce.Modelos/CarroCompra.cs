using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFoodCommerce.Modelos
{
    public class CarroCompra : IEntity
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public int ProductoCodigo { get; set; }

        //[ForeignKey("Codigo")]
        public Producto? Producto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 1.")]
        public int Cantidad { get; set; }

        [Required]
        public int PrecioProductoCodigo { get; set; }

        //[ForeignKey("Codigo")]
        public PrecioProducto? PrecioProducto { get; set; }
    }
}