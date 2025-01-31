using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EFoodCommerce.Utilidades;


namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_ADMINISTRADOR + "," + DS.ROLE_MANTENIMIENTO)]
    public class TipoPrecioController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public TipoPrecioController(IUnidadTrabajo unidadTrabajo)
        {
                _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<TipoPrecio?> UpsertBase(int? codigo)
        {
            TipoPrecio? tipoPrecio = new();
            if (codigo == null)
            {
                return tipoPrecio;
            }
            tipoPrecio = await _unidadTrabajo.TipoPrecio.Obtener(codigo.GetValueOrDefault());
            return tipoPrecio;
        }

        public async Task<IActionResult> Upsert(int? codigo)
        {
            TipoPrecio? tipoPrecio = await UpsertBase(codigo);
            return View(tipoPrecio);
        }

        public async Task UpsertBase(TipoPrecio tipoPrecio)
        {
            if (tipoPrecio.Codigo == 0)
            {
                await _unidadTrabajo.TipoPrecio.Agregar(tipoPrecio);
            }
            else
            {
                _unidadTrabajo.TipoPrecio.Editar(tipoPrecio);
                
            }
            await _unidadTrabajo.Guardar();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TipoPrecio tipoPrecio)
        {
            if(ModelState.IsValid)
            {
                await UpsertBase(tipoPrecio);
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPrecio);
        }

        public async Task<IEnumerable<TipoPrecio>> ObtenerTodosBase()
        {
            return await _unidadTrabajo.TipoPrecio.ObtenerTodos();
        }

        public async Task<bool> DeleteBase(int codigo)
        {
            var tipoPrecioDb = await _unidadTrabajo.TipoPrecio.Obtener(codigo);
            if (tipoPrecioDb == null)
            {
                return false;
            }
            _unidadTrabajo.TipoPrecio.Remover(tipoPrecioDb);
            await _unidadTrabajo.Guardar();
            return true;
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await ObtenerTodosBase();
            return Json(new {data = todos});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int codigo)
        {
            var exito = await DeleteBase(codigo);
            if (exito)
            {
                return Json(new { success = true, message = "Eliminación exitosa" });
            }
            return Json(new { success = false, message = "Error al eliminar el tipo de precio" });
        }

        #endregion

    }
}
