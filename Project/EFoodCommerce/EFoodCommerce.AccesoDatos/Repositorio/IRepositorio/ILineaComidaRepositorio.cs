using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFoodCommerce.AccesoDatos.Repositorio.IRepositorio
{
    public interface ILineaComidaRepositorio : IRepositorio<LineaComida>
    {
        public IEnumerable<SelectListItem> ObtenerLineaComidaDropdownLista();
    }
}
