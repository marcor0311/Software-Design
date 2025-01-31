using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFoodCommerce.Modelos.ViewModels
{
    public class ProductoVM
    {
        public Producto Producto { get; set; } = null!;
        public IEnumerable<SelectListItem>? LineaComidaLista { get; set; }

        public IEnumerable<string>? Precios { get; set; }
    }
}
