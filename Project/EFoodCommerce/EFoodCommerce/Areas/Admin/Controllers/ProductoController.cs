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
    public class ProductoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
                _unidadTrabajo = unidadTrabajo;
                _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ProductoVM?> UpsertBase(int? codigo)
        {
            ProductoVM? productoVM = new()
            {
                Producto = new Producto(),
                LineaComidaLista = _unidadTrabajo.LineaComida.ObtenerLineaComidaDropdownLista()
            };
            if (codigo == null)
            {
                return productoVM;
            }

            var producto = await _unidadTrabajo.Producto.Obtener(codigo.GetValueOrDefault());
            if (producto == null)
            {
                return null;
            }
            productoVM.Producto = producto;
            return productoVM;
        }

        public async Task<IActionResult> Upsert(int? codigo)
        {
            var productoVM = await UpsertBase(codigo);
            if (productoVM == null)
                {
                    return NotFound();
            }
            return View(productoVM);
            
        }

        public async Task<bool> UpsertBase(ProductoVM productoVM)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;


            if (productoVM.Producto.Codigo == 0)
            {
                string upload = webRootPath + DS.ImagenRuta;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload,fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                productoVM.Producto.RutaImagen = fileName + extension;
                await _unidadTrabajo.Producto.Agregar(productoVM.Producto);

            }
            else
            {
                var objProducto = await _unidadTrabajo.Producto.ObtenerPrimero(p=>p.Codigo == productoVM.Producto.Codigo, isTracking:false);
                if (objProducto == null)
                {
                    //return NotFound();
                    return false;
                }
                if (files.Count>0)
                {
                    string upload = webRootPath+DS.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);



                    var anteriorFile = Path.Combine(upload,objProducto.RutaImagen??"");
                    if (System.IO.File.Exists(anteriorFile))
                    {
                        System.IO.File.Delete(anteriorFile);
                    }
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName+extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productoVM.Producto.RutaImagen = fileName + extension;
                }
                else
                {
                    productoVM.Producto.RutaImagen = objProducto.RutaImagen;

                }
                _unidadTrabajo.Producto.Editar(productoVM.Producto);
            }
            await _unidadTrabajo.Guardar();
            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductoVM productoVM)
        {
            if (productoVM.Producto != null && ModelState.IsValid)
            {
                if (!await UpsertBase(productoVM))
                {
                    return NotFound();
                }

                return View("Index");
            }
            productoVM.LineaComidaLista = _unidadTrabajo.LineaComida.ObtenerLineaComidaDropdownLista();
            return View(productoVM);
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosBase()
        {
            return await _unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades:"LineaComida");
        }

        public async Task<bool> DeleteBase(int codigo)
        {
            var productoDb = await _unidadTrabajo.Producto.Obtener(codigo);
            if (productoDb == null)
            {
                return false;
            }
            _unidadTrabajo.Producto.Remover(productoDb);
            await _unidadTrabajo.Guardar();
            return true;
        }

        #region API


        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await ObtenerTodosBase();
            return Json(new {data=todos});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int codigo)
        {
            var exito = await DeleteBase(codigo);
            if (exito)
            {
                return Json(new {success = true, message = "Eliminación exitosa"});
            }
            return Json(new {success = false, message = "Error al eliminar"});
        }

        #endregion
    }

}
