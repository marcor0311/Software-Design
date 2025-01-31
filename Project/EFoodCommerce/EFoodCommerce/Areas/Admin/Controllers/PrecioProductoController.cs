using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EFoodCommerce.Utilidades;
using EFoodCommerce.Modelos.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;


namespace EFoodCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.ROLE_ADMINISTRADOR)]
    public class PrecioProductoController : Controller
    {
        
        private readonly IUnidadTrabajo _unidadTrabajo;
        public PrecioProductoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index(int codigoProducto)
        {
            
            ViewBag.codigoProducto = codigoProducto;
            return View();
        }


        public async Task<PrecioProductoVM?> UpsertBase(int? codigo, int codigoProducto)
        {
            PrecioProductoVM precioProductoVM = new()
            {
                PrecioProducto = new PrecioProducto(),
                TipoPrecioLista = _unidadTrabajo.PrecioProducto.ObtenerTipoPrecioDropdownLista(),
                Producto = await _unidadTrabajo.Producto.Obtener(codigoProducto)??new()
            };
            
            precioProductoVM.PrecioProducto.CodigoProducto = codigoProducto;
            
            if (codigo == null)
            {
                return precioProductoVM;
            }
            var precioProducto = await _unidadTrabajo.PrecioProducto.Obtener(codigo.GetValueOrDefault());
            if(precioProducto == null)
            {
                return null;
            }
            precioProductoVM.PrecioProducto = precioProducto;
            return precioProductoVM;
        }

        
        public async Task<IActionResult> Upsert(int? codigo, int codigoProducto)
        {
            PrecioProductoVM? precioProductoVM = await UpsertBase(codigo, codigoProducto);
            if(precioProductoVM == null)
            {
                return NotFound();
            }
            return View(precioProductoVM);

        }

        public async Task<bool> UpsertBase(PrecioProductoVM precioProductoVM)
        {
            precioProductoVM.TipoPrecioLista = _unidadTrabajo.PrecioProducto.ObtenerTipoPrecioDropdownLista();
            if (precioProductoVM.PrecioProducto.Codigo == 0)
            {
                var duplicado = await _unidadTrabajo.PrecioProducto.ObtenerPrimero(pp => pp.CodigoProducto == precioProductoVM.PrecioProducto.CodigoProducto
                    && pp.CodigoTipoPrecio == precioProductoVM.PrecioProducto.CodigoTipoPrecio);
                if (duplicado != null) return true;
                await _unidadTrabajo.PrecioProducto.Agregar(precioProductoVM.PrecioProducto);
            }
            else
            {
                var duplicado = await _unidadTrabajo.PrecioProducto.ObtenerPrimero(pp => pp.Codigo != precioProductoVM.PrecioProducto.Codigo && pp.CodigoProducto == precioProductoVM.PrecioProducto.CodigoProducto
                    && pp.CodigoTipoPrecio == precioProductoVM.PrecioProducto.CodigoTipoPrecio);
                if (duplicado != null) {
                    _unidadTrabajo.DesacoplarEntidad(duplicado);
                    return true;
                }
                _unidadTrabajo.PrecioProducto.Editar(precioProductoVM.PrecioProducto);
            }
            await _unidadTrabajo.Guardar();
            return false;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PrecioProductoVM precioProductoVM)
        {
            if (precioProductoVM.PrecioProducto != null && ModelState.IsValid)
            {
                if (await UpsertBase(precioProductoVM)) {
                    return View(precioProductoVM);
                }
                var url = $"/Admin/PrecioProducto/Index?codigoProducto={precioProductoVM.Producto.Codigo}";
                return Redirect(url);
            }
            return View(precioProductoVM);
        }

        public async Task<IEnumerable<PrecioProducto>> ObtenerTodosBase(int codigoProducto)
        {
            return await _unidadTrabajo.PrecioProducto.ObtenerPreciosPorCodigoProductoAsync(codigoProducto);
        }

        public async Task<bool> DeleteBase(int codigo)
        {
            var precioProductoDb = await _unidadTrabajo.PrecioProducto.Obtener(codigo);
            if (precioProductoDb == null)
            {
                return false;
            }
            _unidadTrabajo.PrecioProducto.Remover(precioProductoDb);
            await _unidadTrabajo.Guardar();
            return true;
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos(int codigoProducto)
        {
            var todos = await ObtenerTodosBase(codigoProducto);
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
            return Json(new { success = false, message = "Error al eliminar el precio" });
        }

        #endregion

    }
}
