using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFoodCommerce.Modelos.ViewModels
{
    public class PrecioProductoVM
    {
        public PrecioProducto PrecioProducto { get; set; } = null!;
        public IEnumerable<SelectListItem>? TipoPrecioLista { get; set; }
        public Producto Producto { get; set; } = null!;
    }
}
