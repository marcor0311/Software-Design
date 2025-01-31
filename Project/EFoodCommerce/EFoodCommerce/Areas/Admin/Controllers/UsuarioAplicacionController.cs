using System.Security.Claims;
using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.ViewModels;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
namespace EFoodCommerce.Areas.Admin.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_ADMINISTRADOR + "," + DS.ROLE_SEGURIDAD)]
    public class UsuarioAplicacionController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccesor;

        public UsuarioAplicacionController(IUnidadTrabajo unidadTrabajo, UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _unidadTrabajo = unidadTrabajo;
            _userManager = userManager;
            _httpContextAccesor = contextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IEnumerable<UsuarioAplicacion>> ObtenerTodosBase()
        {
            var usuarios = await _unidadTrabajo.UsuarioAplicacion.ObtenerTodos();
            foreach (var usuario in usuarios) {
                var roles =  await _userManager.GetRolesAsync(usuario);
                if (!roles.IsNullOrEmpty()) {
                    usuario.Role = roles.First();
                }
            }
            return usuarios;
        }

        public async Task<bool> BloquearBesbloquearBase(string id)
        {
            var user = await _unidadTrabajo.UsuarioAplicacion.ObtenerFake(id);
            if (user == null) return false;
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            await _unidadTrabajo.Guardar();
            return true;
        }

        public async Task<bool> DeleteBase(string id)
        {
            var user = await _unidadTrabajo.UsuarioAplicacion.ObtenerFake(id);
            if (user == null) return false;
            await _userManager.DeleteAsync(user);
            return true;
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarioLista = await ObtenerTodosBase();
            return Json(new { data = usuarioLista });
        }

        [HttpPost]
        public async Task<IActionResult> BloquearDesbloquear([FromBody] string id)
        {
            var exito = await BloquearBesbloquearBase(id);
            if (exito)
            {
                return Json(new { success = true, message = "Operacion Exitosa" });
            }
            return Json(new { success = false, message = "Error al bloquear/desbloquear el usuario" });
        }

        [HttpPost]
        public async Task<IActionResult> SetRole(string Id, string Code) {
            var user = await _unidadTrabajo.UsuarioAplicacion.ObtenerFake(Id);
            if (user == null) return Json(new { success = false, message = "No user with such an id found" });
            
            if (_httpContextAccesor.HttpContext == null) return Json(new { success = false, message = "????????" });
            var current_user = _httpContextAccesor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (current_user == Id) return Json(new { success = false, message = "You can't change your own role" });
            string? real_role;
            if (Code == "0") real_role = null;
            else if (Code == "1") real_role = "Administrador";
            else if (Code == "2") real_role = "Mantenimiento";
            else if (Code == "3") real_role = "Seguridad";
            else if (Code == "4") real_role = "Consultas";
            else if (Code == "5") real_role = "Tester";
            else return Json(new { success = false, message = "Este rol no existe!" });
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            if (real_role != null) await _userManager.AddToRoleAsync(user, real_role);
            return Json(new { success = true, message = "Operacion Exitosa" });
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            var exito = await DeleteBase(id);
            if (exito)
            {
                return Json(new { success = true, message = "Eliminación exitosa" });
            }
            return Json(new { success = false, message = "Error al eliminar el usuario" });
        }

        #endregion

    }
}
