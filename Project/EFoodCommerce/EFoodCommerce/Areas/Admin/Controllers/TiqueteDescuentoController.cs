using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EFoodCommerce.Utilidades;


namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_ADMINISTRADOR + "," + DS.ROLE_MANTENIMIENTO)]
    public class TiqueteDescuentoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public TiqueteDescuentoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<TiqueteDescuento?> UpsertBase(int? codigo)
        {
            TiqueteDescuento? tiqueteDescuento = new();
            if (codigo == null)
            {
                return tiqueteDescuento;
            }
            tiqueteDescuento = await _unidadTrabajo.TiqueteDescuento.Obtener(codigo.GetValueOrDefault());
            return tiqueteDescuento;
        }

        public async Task<IActionResult> Upsert(int? codigo)
        {
            TiqueteDescuento? tiqueteDescuento = await UpsertBase(codigo);
            if (tiqueteDescuento == null)
            {
                return NotFound();
            }
            return View(tiqueteDescuento);
        }

        public async Task<bool> UpsertBase(TiqueteDescuento tiqueteDescuento)
        {
            if (tiqueteDescuento.Codigo == 0)
            {
                await _unidadTrabajo.TiqueteDescuento.Agregar(tiqueteDescuento);
            }
            else
            {
                if (tiqueteDescuento.Codigo == 10)
                {
                    return false;
                }
                _unidadTrabajo.TiqueteDescuento.Editar(tiqueteDescuento);
            }
            await _unidadTrabajo.Guardar();
            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TiqueteDescuento tiqueteDescuento)
        {
            if (ModelState.IsValid)
            {
                var exito = await UpsertBase(tiqueteDescuento);
                if (exito)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(tiqueteDescuento);
        }

        public async Task<IEnumerable<TiqueteDescuento>> ObtenerTodosBase()
        {
            /*
                Codigo 10 es reservado para el tiquete N/A.
            */
            return await _unidadTrabajo.TiqueteDescuento.ObtenerTodos(p => p.Codigo != 10);
        }

        public async Task<bool> DeleteBase(int codigo)
        {
            var tiqueteDescuentoDb = await _unidadTrabajo.TiqueteDescuento.Obtener(codigo);
            /*
                Codigo 10 es reservado para el tiquete N/A.
            */
            if (tiqueteDescuentoDb == null || codigo == 10)
            {
                return false;
            }
            _unidadTrabajo.TiqueteDescuento.Remover(tiqueteDescuentoDb);
            await _unidadTrabajo.Guardar();
            return true;
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await ObtenerTodosBase();
            return Json(new { data = todos });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int codigo)
        {
            var exito = await DeleteBase(codigo);
            if (exito)
            {
                return Json(new { success = true, message = "Eliminación exitosa" });
            }
            return Json(new { success = false, message = "Error al eliminar el tiquete de descuento" });
        }

        #endregion

    }
}