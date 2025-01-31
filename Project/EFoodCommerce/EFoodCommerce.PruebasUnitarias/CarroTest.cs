using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.EFoodCommerce.Controllers;
using EFoodCommerce.Modelos;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Http;
using Moq;

namespace EFoodCommerce.PruebasUnitarias
{
    public class CarroTest
    {
        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<ICarroCompraRepositorio> _mockCarroCompraRepositorio;
        private Mock<IProductoRepositorio> _mockProductoRepositorio;
        private Mock<IPrecioProductoRepositorio> _mockPrecioProductoRepositorio;
        private Mock<IPedidoRepositorio> _mockPedidoRepositorio;
        private CarroController _carroController;
        
        [SetUp]
        public void Setup()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockCarroCompraRepositorio = new Mock<ICarroCompraRepositorio>();
            _mockProductoRepositorio = new Mock<IProductoRepositorio>();
            _mockPrecioProductoRepositorio = new Mock<IPrecioProductoRepositorio>();
            _mockPedidoRepositorio = new Mock<IPedidoRepositorio>();

            _mockUnidadTrabajo.Setup(u => u.CarroCompra).Returns(_mockCarroCompraRepositorio.Object);
            _mockUnidadTrabajo.Setup(u => u.Producto).Returns(_mockProductoRepositorio.Object);
            _mockUnidadTrabajo.Setup(u => u.PrecioProducto).Returns(_mockPrecioProductoRepositorio.Object);
            _mockUnidadTrabajo.Setup(u => u.Pedido).Returns(_mockPedidoRepositorio.Object);

            _carroController = new CarroController(_mockUnidadTrabajo.Object);
            
            SessionFaker.SetFakeHttpContext(_carroController);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockCarroCompraRepositorio = null!;
            _mockProductoRepositorio = null!;
            _mockPrecioProductoRepositorio = null!;
            _mockPedidoRepositorio = null!;
            _carroController.Dispose();
        }

        [Test]
        public void TestIndexBase()
        {
            var carroCompras = new List<CarroCompra>
            {
                new() { Codigo = 1, ProductoCodigo = 1, PrecioProductoCodigo = 1, Cantidad = 1 },
                new() { Codigo = 2, ProductoCodigo = 2, PrecioProductoCodigo = 2, Cantidad = 2 }
            };
            var productos = new List<Producto>
            {
                new() { Codigo = 1, Nombre = "Producto 1", CodigoLineaComida = 1 },
                new() { Codigo = 2, Nombre = "Producto 2", CodigoLineaComida = 2 }
            };
            var precios1 = new List<PrecioProducto>
            {
                new() { Codigo = 1, CodigoProducto = 1, Precio = 1000, TipoPrecio = new TipoPrecio { Nombre = "Precio 1" } },
                new() { Codigo = 2, CodigoProducto = 1, Precio = 2000, TipoPrecio = new TipoPrecio { Nombre = "Precio 2" } }
            };
            var precios2 = new List<PrecioProducto>
            {
                new() { Codigo = 3, CodigoProducto = 2, Precio = 1000, TipoPrecio = new TipoPrecio { Nombre = "Precio 3" } },
                new() { Codigo = 4, CodigoProducto = 2, Precio = 2000, TipoPrecio = new TipoPrecio { Nombre = "Precio 4" } }
            };

            _mockUnidadTrabajo.Setup(repo => repo.CarroCompra.ObtenerTodos()).Returns(carroCompras);
            _mockProductoRepositorio.Setup(repo => repo.Obtener(1)).ReturnsAsync(productos[0]);
            _mockProductoRepositorio.Setup(repo => repo.Obtener(2)).ReturnsAsync(productos[1]);
            _mockPrecioProductoRepositorio.Setup(repo => repo.Obtener(1)).ReturnsAsync(precios1[0]);
            _mockPrecioProductoRepositorio.Setup(repo => repo.Obtener(2)).ReturnsAsync(precios1[1]);
            _mockPrecioProductoRepositorio.Setup(repo => repo.Obtener(3)).ReturnsAsync(precios2[0]);
            _mockPrecioProductoRepositorio.Setup(repo => repo.Obtener(4)).ReturnsAsync(precios2[1]);
            
            var result = _carroController.IndexBase().Result;

            Assert.That(result.CarroCompraLista, Is.EqualTo(carroCompras));
            Assert.That(result.Pedido, Is.Not.Null);
            Assert.That(result.Pedido.Monto, Is.EqualTo(5000));
        }

        [Test]
        public void TestMasBase()
        {
            var carroCompra = new CarroCompra { Codigo = 1, Cantidad = 1 };

            _mockCarroCompraRepositorio.Setup(repo => repo.Obtener(1)).Returns(carroCompra);
            _mockCarroCompraRepositorio.Setup(repo => repo.Actualizar(carroCompra));

            var result = _carroController.MasBase(1);

            Assert.That(result, Is.True);
            Assert.That(carroCompra.Cantidad, Is.EqualTo(2));
        }

        [Test]
        public void TestMenosBase()
        {
            var carroCompra = new CarroCompra { Codigo = 1, Cantidad = 2 };
            List<CarroCompra> carroCompras = [carroCompra];

            _mockCarroCompraRepositorio.Setup(repo => repo.Obtener(1)).Returns(carroCompra);
            _mockCarroCompraRepositorio.Setup(repo => repo.Actualizar(carroCompra));
            _mockCarroCompraRepositorio.Setup(repo => repo.ObtenerTodos()).Returns(carroCompras);

            var result = _carroController.MenosBase(1);

            Assert.That(result, Is.True);
            Assert.That(carroCompra.Cantidad, Is.EqualTo(1));

            carroCompras = [];

            result = _carroController.MenosBase(1);

            Assert.That(result, Is.True);
            Assert.That(_carroController.HttpContext.Session.GetInt32(DS.ssCarroCompras), Is.EqualTo(0));
        }

        [Test]
        public void TestRemoverBase()
        {
            var carroCompra = new CarroCompra { Codigo = 1, Cantidad = 2 };
            List<CarroCompra> carroCompras = [carroCompra];

            _mockCarroCompraRepositorio.Setup(repo => repo.Remover(1));
            _mockCarroCompraRepositorio.Setup(repo => repo.ObtenerTodos()).Returns(carroCompras);

            _ = _carroController.RemoverBase(1);

            Assert.That(_carroController.HttpContext.Session.GetInt32(DS.ssCarroCompras), Is.EqualTo(0));
        }
    }
}