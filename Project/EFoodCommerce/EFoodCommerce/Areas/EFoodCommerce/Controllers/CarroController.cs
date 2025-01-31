using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.ViewModels;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace EFoodCommerce.Areas.EFoodCommerce.Controllers
{
    [Area("EFoodCommerce")]
    public class CarroController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CarroController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        private async Task ConstruirCarroCompraAsync(CarroCompra carroCompra)
        {
            int CarroCompraProductoCodigo = carroCompra.ProductoCodigo;
            int CarroCompraPrecioProducto = carroCompra.PrecioProductoCodigo;
            var producto = await _unidadTrabajo.Producto.Obtener(CarroCompraProductoCodigo);
            var precioProducto = await _unidadTrabajo.PrecioProducto.Obtener(CarroCompraPrecioProducto);
            carroCompra.Producto = producto;
            carroCompra.PrecioProducto = precioProducto;

        }

        public async Task<CarroCompraVM> IndexBase() 
        {
            CarroCompraVM CarroCompraVM = new()
            {
                CarroCompraLista = _unidadTrabajo.CarroCompra.ObtenerTodos(),
                Pedido = new Pedido()
            };
            CarroCompraVM.Pedido.Monto = 0;
            foreach (var i in CarroCompraVM.CarroCompraLista)
            {
                await ConstruirCarroCompraAsync(i);
                CarroCompraVM.Pedido.Monto += (i.PrecioProducto ?? (new PrecioProducto())).Precio * i.Cantidad;
            }
            return CarroCompraVM;
        }

        public async Task<IActionResult> Index()
        {

            CarroCompraVM CarroCompraVM = await IndexBase();

            return View(CarroCompraVM);
        }

        public bool MasBase(int Codigo)
        {
            var carroCompras = _unidadTrabajo.CarroCompra.Obtener(Codigo);
            if (carroCompras == null)
            {
                return false;
            }
            carroCompras.Cantidad += 1;
            _unidadTrabajo.CarroCompra.Actualizar(carroCompras);
            return true;
        }  

        public IActionResult Mas(int Codigo)
        {
            if (MasBase(Codigo))
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public bool MenosBase(int Codigo) {
            var carroCompras = _unidadTrabajo.CarroCompra.Obtener(Codigo);
            if (carroCompras == null)
            {
                return false;
            }
            if (carroCompras.Cantidad == 1)
            {
                var carroLista = _unidadTrabajo.CarroCompra.ObtenerTodos();
                var numeroProductos = carroLista.Count();
                _unidadTrabajo.CarroCompra.Remover(Codigo);
                HttpContext.Session.SetInt32(DS.ssCarroCompras, numeroProductos - 1);
            }
            else
            {
                carroCompras.Cantidad -= 1;
                _unidadTrabajo.CarroCompra.Actualizar(carroCompras);
            }
            return true;
        }

        public IActionResult Menos(int Codigo)
        {
            if (MenosBase(Codigo))
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task RemoverBase(int Codigo)
        {
            var carroLista = _unidadTrabajo.CarroCompra.ObtenerTodos();
            var numeroProductos = carroLista.Count();
            _unidadTrabajo.CarroCompra.Remover(Codigo);
            await _unidadTrabajo.Guardar();
            HttpContext.Session.SetInt32(DS.ssCarroCompras, numeroProductos - 1);
        }

        public async Task<IActionResult> Remover(int Codigo)
        {
            await RemoverBase(Codigo);
            return RedirectToAction("Index");
        }
    }
}
