using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Controllers;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.ViewModels;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;

namespace EFoodCommerce.PruebasUnitarias
{
    public class HomeTest
    {
        private Mock<IUnidadTrabajo> _unidadTrabajo;
        private Mock<ILineaComidaRepositorio> _lineaComidaRepositorio;
        private Mock<ICarroCompraRepositorio> _carroCompraRepositorio;
        private Mock<IPrecioProductoRepositorio> _precioProductoRepositorio;
        private Mock<IProductoRepositorio> _productoRepositorio;
        private HomeController _homeController;
        [SetUp]
        public void Setup()
        {
            _unidadTrabajo = new Mock<IUnidadTrabajo>();
            _lineaComidaRepositorio = new Mock<ILineaComidaRepositorio>();
            _carroCompraRepositorio = new Mock<ICarroCompraRepositorio>();
            _precioProductoRepositorio = new Mock<IPrecioProductoRepositorio>();
            _productoRepositorio = new Mock<IProductoRepositorio>();

            _unidadTrabajo.Setup(u => u.LineaComida).Returns(_lineaComidaRepositorio.Object);
            _unidadTrabajo.Setup(u => u.CarroCompra).Returns(_carroCompraRepositorio.Object);
            _unidadTrabajo.Setup(u => u.PrecioProducto).Returns(_precioProductoRepositorio.Object);
            _unidadTrabajo.Setup(u => u.Producto).Returns(_productoRepositorio.Object);

            _homeController = new HomeController(_unidadTrabajo.Object);
            
            SessionFaker.SetFakeHttpContext(_homeController);
        }

        [TearDown]
        public void TearDown()
        {
            _unidadTrabajo = null!;
            _lineaComidaRepositorio = null!;
            _carroCompraRepositorio = null!;
            _precioProductoRepositorio = null!;
            _productoRepositorio = null!;
            _homeController.Dispose();
        }

        [Test]
        public void TestDetalleBase1()
        {
            var producto = new Producto
            {
                Codigo = 1,
                Nombre = "Producto 1",
                CodigoLineaComida = 1
            };
            var lineaComida = new LineaComida
            {
                Codigo = 1,
                Nombre = "Linea 1"
            };
            var precioProducto = new PrecioProducto
            {
                Codigo = 1,
                CodigoProducto = 1,
                Precio = 10
            };
            List<SelectListItem> precios = new()
            {
                new SelectListItem
                {
                    Value = "1",
                    Text = "10"
                }
            };

            _productoRepositorio.Setup(p => p.Obtener(It.IsAny<int>())).ReturnsAsync(producto);
            _lineaComidaRepositorio.Setup(l => l.Obtener(1)).ReturnsAsync(lineaComida);
            _precioProductoRepositorio.Setup(p => p.ObtenerPreciosPorCodigoProductoAsyncDropdownLista(It.IsAny<int>())).ReturnsAsync(precios);

            var resultado = _homeController.DetalleBase(1).Result;

            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado.Producto, Is.EqualTo(producto));
            Assert.That(resultado.NombreLineaComida, Is.EqualTo("Linea 1"));
            Assert.That(resultado.Precios, Is.EqualTo(precios));
            Assert.That(resultado.CarroCompra, Is.Not.Null);
            Assert.That(resultado.CarroCompra.ProductoCodigo, Is.EqualTo(1));
        }

        [Test]
        public void TestDetalleBase2()
        {
            CarroCompra carroCompra = new()
            {
                ProductoCodigo = 1
            };
            CarroCompraVM? carroCompraVM = new() {
                CarroCompra = carroCompra
            };

            _ = _homeController.DetalleBase(carroCompraVM);

            Assert.That(_homeController.HttpContext.Session.GetInt32(DS.ssCarroCompras), Is.EqualTo(1));
        }
    }
}