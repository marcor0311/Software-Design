using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFoodCommerce.Modelos.ViewModels
{
    public class CarroCompraVM
    {
        public Producto? Producto { get; set; }
        public string? NombreLineaComida { get; set; }

        public CarroCompra? CarroCompra { get; set; }

        public IEnumerable<SelectListItem>? Precios { get; set; }

        public IEnumerable<CarroCompra>? CarroCompraLista { get; set; }
        public Pedido? Pedido { get; set; }
    }
}