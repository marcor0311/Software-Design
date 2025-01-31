using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EFoodCommerce.Utilidades;


namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_ADMINISTRADOR + "," + DS.ROLE_MANTENIMIENTO)]
    public class LineaComidaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public LineaComidaController(IUnidadTrabajo unidadTrabajo)
        {
                _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<LineaComida?> UpsertBase(int? codigo)
        {
            LineaComida? lineaComida;
            if (codigo == null)
            {
                lineaComida = new();
                return lineaComida;
            }
            //actualizar 
            lineaComida = await _unidadTrabajo.LineaComida.Obtener(codigo.GetValueOrDefault());
            return lineaComida;
        }

        public async Task<IActionResult> Upsert(int? codigo)
        {
            
            LineaComida? lineaComida = await UpsertBase(codigo);
           
            if (lineaComida == null)
            {
                return NotFound();

            }
            return View(lineaComida);

        }

        public async Task UpsertBase(LineaComida lineaComida)
        {
            if (lineaComida.Codigo == 0)
            {
                await _unidadTrabajo.LineaComida.Agregar(lineaComida);
            }
            else
            {
                _unidadTrabajo.LineaComida.Editar(lineaComida);
                
            }
            await _unidadTrabajo.Guardar();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(LineaComida lineaComida)
        {
            if(ModelState.IsValid)
            {
                await UpsertBase(lineaComida);
                return RedirectToAction(nameof(Index));
            }
            return View(lineaComida);
        }

        public async Task<IEnumerable<LineaComida>> ObtenerTodosBase()
        {
            return await _unidadTrabajo.LineaComida.ObtenerTodos();
        }

        public async Task<bool> DeleteBase(int codigo)
        {
            var lineaComidaDb = await _unidadTrabajo.LineaComida.Obtener(codigo);
            if (lineaComidaDb == null)
            {
                return false;
            }
            _unidadTrabajo.LineaComida.Remover(lineaComidaDb);
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
            var resultado = await DeleteBase(codigo);
            if (resultado)
            {
                return Json(new { success = true, message = "Exito al borrar" });
            }
            return Json(new { success = false, message = "Error al eliminar" });
        }

        #endregion

    }
}
