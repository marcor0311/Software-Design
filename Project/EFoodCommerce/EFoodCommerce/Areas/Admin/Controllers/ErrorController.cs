using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_ADMINISTRADOR + "," + DS.ROLE_CONSULTAS)]
    public class ErrorController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ErrorController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IEnumerable<Modelos.Error>> ObtenerTodosBase()
        {
            return await _unidadTrabajo.Error.ObtenerTodos();
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var errorlista = await ObtenerTodosBase();
            
            return Json(new { data = errorlista });
        }

        #endregion
    }
}
