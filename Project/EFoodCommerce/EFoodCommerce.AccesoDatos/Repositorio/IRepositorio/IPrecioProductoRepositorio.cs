using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFoodCommerce.AccesoDatos.Repositorio.IRepositorio
{
    public interface IPrecioProductoRepositorio : IRepositorio<PrecioProducto>
    {
        public Task<IEnumerable<PrecioProducto>> ObtenerPreciosPorCodigoProductoAsync(int codigoProducto);
        public Task<IEnumerable<SelectListItem>> ObtenerPreciosPorCodigoProductoAsyncDropdownLista(int codigoProducto);
        IEnumerable<SelectListItem> ObtenerTipoPrecioDropdownLista();
    }
}
