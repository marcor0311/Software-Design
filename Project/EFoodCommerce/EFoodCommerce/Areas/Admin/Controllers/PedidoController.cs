using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EFoodCommerce.Modelos;



namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_ADMINISTRADOR + "," + DS.ROLE_CONSULTAS)]
    public class PedidoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public PedidoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IEnumerable<Pedido>> ObtenerTodosBase()
        {
            return await _unidadTrabajo.Pedido.ObtenerTodos();
        }

        public async Task<bool> DeleteBase(int codigo)
        {
            var pedidoDb = await _unidadTrabajo.Pedido.Obtener(codigo);
            if (pedidoDb == null)
            {
                return false;
            }
            _unidadTrabajo.Pedido.Remover(pedidoDb);
            await _unidadTrabajo.Guardar();
            return true;
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var pedidosLista = await ObtenerTodosBase();
            return Json(new { data = pedidosLista });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int codigo)
        {
            var exito = await DeleteBase(codigo);
            if (exito)
            {
                return Json(new { success = true, message = "Eliminación exitosa" });
            }
            return Json(new { success = false, message = "Error al eliminar el pedido" });
        }

        #endregion
    }
}
