using System.Linq.Expressions;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;

namespace EFoodCommerce.PruebasUnitarias
{
    public class VerProductosTest
    {
        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<IProductoRepositorio> _mockProductoRepositorio;
        private Mock<ILineaComidaRepositorio> _mockLineaComidaRepositorio;
        private Mock<IPrecioProductoRepositorio> _mockPrecioProductoRepositorio;
        private VerProductosController _verProductosController;
        [SetUp]
        public void Setup()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockProductoRepositorio = new Mock<IProductoRepositorio>();
            _mockLineaComidaRepositorio = new Mock<ILineaComidaRepositorio>();
            _mockPrecioProductoRepositorio = new Mock<IPrecioProductoRepositorio>();
            _mockUnidadTrabajo.Setup(u => u.Producto).Returns(_mockProductoRepositorio.Object);
            _mockUnidadTrabajo.Setup(u => u.LineaComida).Returns(_mockLineaComidaRepositorio.Object);
            _mockUnidadTrabajo.Setup(u => u.PrecioProducto).Returns(_mockPrecioProductoRepositorio.Object);
            _verProductosController = new VerProductosController(_mockUnidadTrabajo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockProductoRepositorio = null!;
            _mockLineaComidaRepositorio = null!;
            _mockPrecioProductoRepositorio = null!;
            _verProductosController.Dispose();
        }

        [Test]
        public void TestIndexBase()
        {
            List<SelectListItem> lista = [];
            for (int i = 0; i < 5; i++)
            {
                lista.Add(new SelectListItem { Value = i.ToString(), Text = $"Item {i}" });
            }
            _mockLineaComidaRepositorio.Setup(repo => repo.ObtenerLineaComidaDropdownLista()).Returns(lista);

            var result = _verProductosController.IndexBase();
            Assert.That(result, Is.EqualTo(lista));
        }

        [Test]
        public void TestObtenerTodosBase()
        {
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
                new() { Codigo = 3, CodigoProducto = 2, Precio = 3000, TipoPrecio = new TipoPrecio { Nombre = "Precio 3" } },
                new() { Codigo = 4, CodigoProducto = 2, Precio = 4000, TipoPrecio = new TipoPrecio { Nombre = "Precio 4" } }
            };

            _mockUnidadTrabajo.Setup(repo => repo.Producto.ObtenerTodos(
                It.IsAny<Expression<Func<Producto, bool>>>(),
                It.IsAny<Func<IQueryable<Producto>, IOrderedQueryable<Producto>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
             )).ReturnsAsync(productos);

            _mockUnidadTrabajo.Setup(repo => repo.PrecioProducto.ObtenerPreciosPorCodigoProductoAsync(1))
                .ReturnsAsync(precios1);
            _mockUnidadTrabajo.Setup(repo => repo.PrecioProducto.ObtenerPreciosPorCodigoProductoAsync(2))
                .ReturnsAsync(precios2);
            
            var result = _verProductosController.ObtenerTodosBase().Result;

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.ElementAt(0).Producto.Codigo, Is.EqualTo(1));
            Assert.That(result.ElementAt(1).Producto.Codigo, Is.EqualTo(2));
            Assert.That(result.ElementAt(0).Precios, Is.Not.Null);
            Assert.That(result.ElementAt(1).Precios, Is.Not.Null);
            Assert.That(result.ElementAt(0).Precios.Count(), Is.EqualTo(2));
            Assert.That(result.ElementAt(1).Precios.Count(), Is.EqualTo(2));
            Assert.That(result.ElementAt(0).Precios.ElementAt(0), Is.EqualTo("Precio 1 - $1000"));
            Assert.That(result.ElementAt(0).Precios.ElementAt(1), Is.EqualTo("Precio 2 - $2000"));
            Assert.That(result.ElementAt(1).Precios.ElementAt(0), Is.EqualTo("Precio 3 - $3000"));
            Assert.That(result.ElementAt(1).Precios.ElementAt(1), Is.EqualTo("Precio 4 - $4000"));
        }

    }
}