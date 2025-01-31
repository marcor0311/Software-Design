using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_TESTER)]
    public class TesterController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public TesterController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        
        public IActionResult Index()
        {
            return View();
        }


        #region API
        
        [HttpGet]
        public IActionResult RunError()
        {
            throw new Exception("Un tester acaba de tirar un error al propósito!");
        }

        #endregion
    }
}
