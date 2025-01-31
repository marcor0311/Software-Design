using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.ViewModels;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_ADMINISTRADOR + "," + DS.ROLE_CONSULTAS)]
    public class VerProductosController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public VerProductosController(IUnidadTrabajo unidadTrabajo)
        {
                _unidadTrabajo = unidadTrabajo;
        }

        public IEnumerable<SelectListItem> IndexBase() {
            return _unidadTrabajo.LineaComida.ObtenerLineaComidaDropdownLista();
        }
            
        public IActionResult Index()
        {
            var todo = IndexBase();
            return View(todo);
        }

        public async Task<IEnumerable<ProductoVM>> ObtenerTodosBase()
        {
            var todos = await _unidadTrabajo.Producto.ObtenerTodos();
            var todosVM = new List<ProductoVM>();
            foreach (var prod in todos)
            {
                var prodVM = new ProductoVM
                {
                    Producto = prod,
                    
                };
                var precios = await _unidadTrabajo.PrecioProducto.ObtenerPreciosPorCodigoProductoAsync(prod.Codigo);
                List<string> preciosString = [];
                for (int i = 0; i < precios.Count(); i++)
                {
                    var tipo = precios.ElementAt(i).TipoPrecio?.Nombre;
                    var precio = precios.ElementAt(i).Precio.ToString();
                    preciosString.Add($"{tipo} - ${precio}");
                }
                prodVM.Precios = preciosString;
                todosVM.Add(prodVM);
            }
            return todosVM;
        }

        #region


        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todosVM = await ObtenerTodosBase();
            return Json(new {data=todosVM});
        }

        #endregion
    }

}
