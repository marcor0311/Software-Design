using EFoodCommerce.Modelos.Especificaciones;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFoodCommerce.Modelos.ViewModels
{
    public class MenuVM 
    {
        public IEnumerable<SelectListItem>? LineaComidaLista { get; set; }
        public PagedList<Producto> Productos { get; set; } = null!;
    }
}
