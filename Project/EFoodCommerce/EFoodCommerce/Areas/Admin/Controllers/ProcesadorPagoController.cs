using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.ViewModels;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = DS.ROLE_ADMINISTRADOR + "," + DS.ROLE_MANTENIMIENTO)]
    public class ProcesadorPagoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public ProcesadorPagoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ProcesadorPagoVM?> UpsertBase(int? codigo)
        {
            ProcesadorPagoVM? procesadorPagoVM = new() {
                ProcesadorPago = new ProcesadorPago(),
                TarjetasLista = await _unidadTrabajo.Tarjeta.ObtenerTodos(),
                TarjetasSeleccionadasLista = _unidadTrabajo.TarjetaProcesadorPago.BuscarPorProcesadorPago(codigo.GetValueOrDefault())
            };
            if (codigo == null)
            {
                return procesadorPagoVM;
            }
            var procesadorPago = await _unidadTrabajo.ProcesadorPago.Obtener(codigo.GetValueOrDefault());
            if (procesadorPago == null)
            {
                return null;
            }
            procesadorPagoVM.ProcesadorPago = procesadorPago;
            return procesadorPagoVM;
        }

        public async Task<IActionResult> Upsert(int? codigo)
        {
            ProcesadorPagoVM? procesadorPagoVM = await UpsertBase(codigo);
            if (procesadorPagoVM == null)
            {
                return NotFound();
            }
            return View(procesadorPagoVM);
        }

        public async Task UpsertBase(ProcesadorPagoVM procesadorPagoVM) {
            procesadorPagoVM.TarjetasSeleccionadasLista ??= [];
            if (procesadorPagoVM.ProcesadorPago.Codigo == 0)
            {
                await _unidadTrabajo.ProcesadorPago.Agregar(procesadorPagoVM.ProcesadorPago);
                await _unidadTrabajo.Guardar();
                _unidadTrabajo.TarjetaProcesadorPago.EliminarPorProcesadorPago(procesadorPagoVM.ProcesadorPago.Codigo);
                await _unidadTrabajo.Guardar();
                if (procesadorPagoVM.ProcesadorPago.Tipo == "debito/credito") _unidadTrabajo.TarjetaProcesadorPago.AgregarPorProcesadorPago(procesadorPagoVM.ProcesadorPago.Codigo, procesadorPagoVM.TarjetasSeleccionadasLista);
            }
            else
            {
                _unidadTrabajo.ProcesadorPago.Editar(procesadorPagoVM.ProcesadorPago);
                _unidadTrabajo.TarjetaProcesadorPago.EliminarPorProcesadorPago(procesadorPagoVM.ProcesadorPago.Codigo);
                await _unidadTrabajo.Guardar();
                if (procesadorPagoVM.ProcesadorPago.Tipo == "debito/credito") _unidadTrabajo.TarjetaProcesadorPago.AgregarPorProcesadorPago(procesadorPagoVM.ProcesadorPago.Codigo, procesadorPagoVM.TarjetasSeleccionadasLista);
            }
            await _unidadTrabajo.Guardar();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProcesadorPagoVM procesadorPagoVM)
        {
            if (ModelState.IsValid)
            {
                await UpsertBase(procesadorPagoVM);
                return RedirectToAction(nameof(Index));
            }
            procesadorPagoVM.TarjetasLista = await _unidadTrabajo.Tarjeta.ObtenerTodos();
            procesadorPagoVM.TarjetasSeleccionadasLista = _unidadTrabajo.TarjetaProcesadorPago.BuscarPorProcesadorPago(procesadorPagoVM.ProcesadorPago.Codigo);
            return View(procesadorPagoVM);
        }

        public async Task<IEnumerable<ProcesadorPago>> ObtenerTodosBase()
        {
            return await _unidadTrabajo.ProcesadorPago.ObtenerTodos();
        }

        public async Task<bool> DeleteBase(int codigo)
        {
            var procesadorPagoDb = await _unidadTrabajo.ProcesadorPago.Obtener(codigo);
            if (procesadorPagoDb == null)
            {
                return false;
            }
            _unidadTrabajo.ProcesadorPago.Remover(procesadorPagoDb);
            await _unidadTrabajo.Guardar();
            return true;
        }

        public async Task<bool> ValidarNombreAsyncBase(ProcesadorPago procesadorPago)
        {
            var codigo = procesadorPago.Codigo;
            var nombre = procesadorPago.NombreProcesador;
            var nuevoProcesadorPago = await _unidadTrabajo.ProcesadorPago.ObtenerPrimero(p => p.NombreProcesador == nombre && p.Codigo != codigo);
            if (nuevoProcesadorPago == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ValidarTipoAsyncBase(ProcesadorPago procesadorPago)
        {
            var codigo = procesadorPago.Codigo;
            var tipo = procesadorPago.Tipo;
            if (tipo.ToLower() != "efectivo")
            {
                return true;
            }
            var nuevoProcesadorPago = await _unidadTrabajo.ProcesadorPago.ObtenerPrimero(p => p.Tipo == tipo && p.Codigo != codigo);
            if (nuevoProcesadorPago == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ValidarEstadoAsyncBase(ProcesadorPago procesadorPago)
        {
            var codigo = procesadorPago.Codigo;
            var tipo = procesadorPago.Tipo;
            var activo = procesadorPago.Estado;
            if (!activo)
            {
                return true;
            }
            var nuevoProcesadorPago = await _unidadTrabajo.ProcesadorPago.ObtenerPrimero(p => p.Estado && p.Tipo == tipo && p.Codigo != codigo);
            if (nuevoProcesadorPago == null)
            {
                return true;
            }
            return false;
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
            return Json(new { success = false, message = "Error al eliminar" });
        }

        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> ValidarNombreAsync(ProcesadorPago procesadorPago)
        {
            if (await ValidarNombreAsyncBase(procesadorPago))
            {
                return Json(true);
            }
            var nombre = procesadorPago.NombreProcesador;
            return Json($"Ya existe un procesador con el nombre {nombre}!");
        }

        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> ValidarTipoAsync(ProcesadorPago procesadorPago)
        {
            if (await ValidarTipoAsyncBase(procesadorPago))
            {
                return Json(true);
            }
            var tipo = procesadorPago.Tipo;
            return Json($"Ya existe un procesador para {tipo}!");
        }

        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> ValidarEstadoAsync(ProcesadorPago procesadorPago)
        {
            if (await ValidarEstadoAsyncBase(procesadorPago))
            {
                return Json(true);
            }
            var tipo = procesadorPago.Tipo;
            return Json($"Ya existe un procesador activo para {tipo}!");
        }

        #endregion

    }
}