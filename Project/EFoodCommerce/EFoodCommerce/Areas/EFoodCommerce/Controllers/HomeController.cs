using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.Especificaciones;
using EFoodCommerce.Modelos.ViewModels;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EFoodCommerce.Controllers
{
    [Area("EFoodCommerce")]
    public class HomeController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public HomeController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index(int pageNumber = 1, string busqueda = "", string busquedaActual = "", int lineaActual = -1)
        {

            if (!string.IsNullOrEmpty(busqueda))
            {
                pageNumber = 1;
            }
            else
            {
                busqueda = busquedaActual;
            }

            ViewData["BusquedaActual"] = busqueda;


            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            Parametros parametros = new()
            {
                PageNumber = pageNumber,
                PageSize = 4
            };

            if (lineaActual != -1)
            {
                ViewData["LineaActual"] = lineaActual.ToString();
            }
            else
            {
                ViewData["LineaActual"] = "-1";
            }

            var lineasDisponibles = _unidadTrabajo.LineaComida.ObtenerLineaComidaDropdownLista();

            ViewData["LineaComidaLista"] = lineasDisponibles;

            var resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros, p => (string.IsNullOrEmpty(busqueda) || p.Nombre.Contains(busqueda)) && (lineaActual == -1 || p.CodigoLineaComida == lineaActual));

            ViewData["TotalPaginas"] = resultado.MetaData.TotalPages;
            ViewData["TotalRegistros"] = resultado.MetaData.TotalCount;
            ViewData["PageSize"] = resultado.MetaData.PageSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previo"] = "disabled";
            ViewData["Siguiente"] = "";

            if (pageNumber > 1)
            {
                ViewData["Previo"] = "";
            }
            if (resultado.MetaData.TotalPages <= pageNumber)
            {
                ViewData["Siguiente"] = "disabled";
            }

            return View(resultado);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<CarroCompraVM?> DetalleBase(int id)
        {
            CarroCompraVM CarroCompraVM = new();
            var producto = await _unidadTrabajo.Producto.Obtener(id);
            if (producto == null)
            {
                return null;
            }
            CarroCompraVM.Producto = producto;
            var linea = await _unidadTrabajo.LineaComida.Obtener(producto.CodigoLineaComida);
            CarroCompraVM.NombreLineaComida = linea != null ? linea.Nombre : "";
            CarroCompraVM.Precios = await _unidadTrabajo.PrecioProducto.ObtenerPreciosPorCodigoProductoAsyncDropdownLista(id);
            CarroCompraVM.CarroCompra = new CarroCompra()
            {
                ProductoCodigo = CarroCompraVM.Producto.Codigo,
            };
            return CarroCompraVM;
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            CarroCompraVM? CarroCompraVM = await DetalleBase(id);
            if (CarroCompraVM == null)
            {
                return NotFound();
            }
            return View(CarroCompraVM);
        }

        public async Task DetalleBase(CarroCompraVM carroCompraVM) {
            if (carroCompraVM.CarroCompra == null) return;
            carroCompraVM.CarroCompra.Codigo = 0;
            _unidadTrabajo.CarroCompra.Agregar(carroCompraVM.CarroCompra);
            int numeroProductos = HttpContext.Session.GetInt32(DS.ssCarroCompras) ?? 0;
            HttpContext.Session.SetInt32(DS.ssCarroCompras, numeroProductos + 1);
            await _unidadTrabajo.Guardar();
            TempData[DS.ssCarroCompras] = "Producto Agregado al Carrito";
        }

        [HttpPost]
        public async Task<IActionResult> Detalle(CarroCompraVM carroCompraVM)
        {
            if (carroCompraVM.CarroCompra != null && ModelState.IsValid) {
                await DetalleBase(carroCompraVM);
                return RedirectToAction("Index");
            }
            return View(carroCompraVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
