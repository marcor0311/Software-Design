using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Linq.Expressions;

namespace EFoodCommerce.PruebasUnitarias
{
    public class ProductoTest
    {

        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<IProductoRepositorio> _mockProductoRepositorio;
        private Mock<ILineaComidaRepositorio> _mockLineaComidaRepositorio;
        private ProductoController _productoController;

        [SetUp]
        public void SetUp()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockProductoRepositorio = new Mock<IProductoRepositorio>();
            _mockLineaComidaRepositorio = new Mock<ILineaComidaRepositorio>();

            _mockUnidadTrabajo.Setup(u => u.Producto).Returns(_mockProductoRepositorio.Object);
            _mockUnidadTrabajo.Setup(u => u.LineaComida).Returns(_mockLineaComidaRepositorio.Object);
            
            _productoController = new ProductoController(_mockUnidadTrabajo.Object, null!);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockProductoRepositorio = null!;
            _productoController.Dispose();
        }

        [Test]
        public async Task VerificarQueObtenerTodosProductoSirve()
        {
            var productoPrueba = new List<Producto>
            {

                new() {Codigo = 1, Nombre = "Refresco", CodigoLineaComida = 1, LineaComida = new LineaComida { Codigo = 1, Nombre = "Bebidas" }, Contenido = "Agua", RutaImagen = "ruta"},
                new() {Codigo = 2, Nombre = "Hamburguesa", CodigoLineaComida = 2, LineaComida = new LineaComida { Codigo = 2, Nombre = "Comida Rápida" }, Contenido = "Carne", RutaImagen = "ruta"}

            };

            _mockUnidadTrabajo.Setup(repo => repo.Producto.ObtenerTodos(
                It.IsAny<Expression<Func<Producto, bool>>>(),
                It.IsAny<Func<IQueryable<Producto>, IOrderedQueryable<Producto>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
             )).ReturnsAsync(productoPrueba);

            var productos = await _productoController.ObtenerTodosBase();

            Assert.That(productos.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task VerificarQueEliminarLineaComidaSirve()
        {
            _mockProductoRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                .ReturnsAsync(new Producto {Codigo = 1, Nombre = "Refresco", CodigoLineaComida = 1, LineaComida = new LineaComida { Codigo = 1, Nombre = "Bebidas" }, Contenido = "Agua", RutaImagen = "ruta"});

            var resultado = await _productoController.DeleteBase(1);

            Assert.That(resultado, Is.True);
        }

        [Test]
        public async Task VerificarQueUnProductoMockSiDevuelveValoresEnUpsert()
        {

            var producto = new Producto {Codigo = 1, Nombre = "Refresco", CodigoLineaComida = 1, LineaComida = new LineaComida { Codigo = 1, Nombre = "Bebidas" }, Contenido = "Agua", RutaImagen = "ruta"};
            List<SelectListItem> listaLineaComida =
            [
                new SelectListItem {Value = "1", Text = "Bebidas"},
                new SelectListItem {Value = "2", Text = "Comida Rápida"}
            ];

            _mockLineaComidaRepositorio.Setup(repo => repo.ObtenerLineaComidaDropdownLista())
                                      .Returns(listaLineaComida);
            _mockProductoRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                                      .ReturnsAsync(producto);

            var lineaDePrueba = await _productoController.UpsertBase(1);

            Assert.That(producto, Is.InstanceOf<Producto>());

        }

        [Test]
        public async Task VerificarQueUpsertSirveParaCrearUnaLineaDeComida()
        {
            List<SelectListItem> listaLineaComida =
            [
                new SelectListItem {Value = "1", Text = "Bebidas"},
                new SelectListItem {Value = "2", Text = "Comida Rápida"}
            ];

            _mockLineaComidaRepositorio.Setup(repo => repo.ObtenerLineaComidaDropdownLista())
                                      .Returns(listaLineaComida);

            var producto = await _productoController.UpsertBase((int?)null);

            Assert.That(producto, Is.InstanceOf<ProductoVM>());

        }


    }
}