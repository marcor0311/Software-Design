using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EFoodCommerce.Utilidades;


namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_ADMINISTRADOR)]
    public class TarjetaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public TarjetaController(IUnidadTrabajo unidadTrabajo)
        {
                _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<Tarjeta?> UpsertBase(int? codigo)
        {
            Tarjeta? tarjeta = new();
            if (codigo == null)
            {
                return tarjeta;
            }
            tarjeta = await _unidadTrabajo.Tarjeta.Obtener(codigo.GetValueOrDefault());
            return tarjeta;
        }
        
        public async Task<IActionResult> Upsert(int? codigo)
        {
            Tarjeta? tarjeta = await UpsertBase(codigo);
            if (tarjeta == null)
            {
                return NotFound();
            }
            return View(tarjeta);
        }

        public async Task UpsertBase(Tarjeta tarjeta)
        {
            if (tarjeta.Codigo == 0)
            {
                await _unidadTrabajo.Tarjeta.Agregar(tarjeta);
            }
            else
            {
                _unidadTrabajo.Tarjeta.Editar(tarjeta);
                
            }
            await _unidadTrabajo.Guardar();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Tarjeta tarjeta)
        {
            if(ModelState.IsValid)
            {
                await UpsertBase(tarjeta);
                return RedirectToAction(nameof(Index));
            }
            return View(tarjeta);
        }

        public async Task<IEnumerable<Tarjeta>> ObtenerTodosBase()
        {
            return await _unidadTrabajo.Tarjeta.ObtenerTodos();
        }

        public async Task<bool> DeleteBase(int codigo)
        {
            var tarjetaDb = await _unidadTrabajo.Tarjeta.Obtener(codigo);
            if (tarjetaDb == null)
            {
                return false;
            }
            _unidadTrabajo.Tarjeta.Remover(tarjetaDb);
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
            return Json(new { success = false, message = "Error al eliminar" });
        }

        #endregion

    }
}
