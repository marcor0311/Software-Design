using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_ADMINISTRADOR + "," +  DS.ROLE_CONSULTAS)]
    public class BitacoraController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public BitacoraController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IEnumerable<Bitacora>> ObtenerTodosBase()
        {
            return await _unidadTrabajo.Bitacora.ObtenerTodos();
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var bitacoralista = await ObtenerTodosBase();
            return Json(new { data = bitacoralista });
        }

        #endregion
    }
}
