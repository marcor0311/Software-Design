using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using Moq;

namespace EFoodCommerce.PruebasUnitarias
{
    public class PrecioProductoTest
    {
        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<IPrecioProductoRepositorio> _mockPrecioProductoRepositorio;
        private PrecioProductoController _precioProductoController;

        [SetUp]
        public void Setup()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockPrecioProductoRepositorio = new Mock<IPrecioProductoRepositorio>();

            _mockUnidadTrabajo.Setup(u => u.PrecioProducto).Returns(_mockPrecioProductoRepositorio.Object);

            _precioProductoController = new PrecioProductoController(_mockUnidadTrabajo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockPrecioProductoRepositorio = null!;
            _precioProductoController.Dispose();
        }

        [Test]
        public async Task VerificarQueUnPrecioProductoMockSiDevuelveValoresEnUpsert()
        {
            int codigoProducto = 1;
            int codigoPrecioProducto = 1;
            var precioProducto = new PrecioProducto
            {
                Codigo = codigoPrecioProducto,
                CodigoProducto = codigoProducto,
                CodigoTipoPrecio = 1,
                Precio = 100
            };

            _mockPrecioProductoRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                                          .ReturnsAsync(precioProducto);
            _mockUnidadTrabajo.Setup(u => u.Producto.Obtener(It.IsAny<int>()))
                              .ReturnsAsync(new Producto { Codigo = codigoProducto });

            var resultado = await _precioProductoController.UpsertBase(codigoPrecioProducto, codigoProducto);

            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado, Is.InstanceOf<PrecioProductoVM>());
            var precioProductoVM = resultado;
            Assert.That(precioProductoVM, Is.Not.Null);
            Assert.That(precioProductoVM.Producto.Codigo, Is.EqualTo(codigoProducto));
            Assert.That(precioProductoVM.PrecioProducto.Codigo, Is.EqualTo(codigoPrecioProducto));
        }

        [Test]
        public async Task VerificarQueUpsertSirveParaCrearUnPrecioProducto()
        {
            int codigoProducto = 1;
            var precioProductoVM = new PrecioProductoVM
            {
                PrecioProducto = new PrecioProducto(),
                Producto = new Producto { Codigo = codigoProducto },
                TipoPrecioLista = new SelectList(new[] { "Tipo1", "Tipo2" })
            };

            _mockPrecioProductoRepositorio.Setup(repo => repo.ObtenerTipoPrecioDropdownLista())
                                        .Returns(precioProductoVM.TipoPrecioLista);
            _mockUnidadTrabajo.Setup(u => u.Producto.Obtener(It.IsAny<int>()))
                            .ReturnsAsync(precioProductoVM.Producto);

            var resultado = await _precioProductoController.Upsert(precioProductoVM);

            Assert.That(resultado, Is.Not.Null);
            if (resultado is ViewResult viewResult)
            {
                Assert.That(viewResult.Model, Is.SameAs(precioProductoVM));
            }
        }


        [Test]
        public async Task VerificarQueObtenerTodosSirve()
        {
            int codigoProducto = 1;
            var preciosProductos = new List<PrecioProducto>
            {
                new() { Codigo = 1, CodigoProducto = codigoProducto, CodigoTipoPrecio = 1, Precio = 100 },
                new() { Codigo = 2, CodigoProducto = codigoProducto, CodigoTipoPrecio = 2, Precio = 200 }
            };

            _mockUnidadTrabajo.Setup(repo => repo.PrecioProducto.ObtenerPreciosPorCodigoProductoAsync(It.IsAny<int>()))
                              .ReturnsAsync(preciosProductos);

            var resultado = await _precioProductoController.ObtenerTodosBase(codigoProducto);

            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado.Count(), Is.EqualTo(preciosProductos.Count));
            Assert.That(resultado.All(pp => pp.CodigoProducto == codigoProducto), Is.True);
        }

        [Test]
        public async Task VerificarQueDeleteBaseFunciona()
        {
            int codigoPrecioProducto = 1;

            _mockPrecioProductoRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                                          .ReturnsAsync(new PrecioProducto { Codigo = codigoPrecioProducto });

            var resultado = await _precioProductoController.DeleteBase(codigoPrecioProducto);

            Assert.That(resultado, Is.True);
        }
    }
}