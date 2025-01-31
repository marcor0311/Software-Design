using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using Moq;
using System.Linq.Expressions;
namespace EFoodCommerce.PruebasUnitarias
{
    public class TipoPrecioTest
    {
        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<ITipoPrecioRepositorio> _mockTipoPrecioRepositorio;
        private TipoPrecioController _tipoPrecioController;

        [SetUp]
        public void Setup()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockTipoPrecioRepositorio = new Mock<ITipoPrecioRepositorio>();
            
            _mockUnidadTrabajo.Setup(u => u.TipoPrecio).Returns(_mockTipoPrecioRepositorio.Object);

            _tipoPrecioController = new TipoPrecioController(_mockUnidadTrabajo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockTipoPrecioRepositorio = null!;
            _tipoPrecioController.Dispose();
        }

        [Test]
        public async Task VerificarQueUnPrecioMockSiDevuelveValoresEnUpsert()
        {
            
            var tipoPrecio = new TipoPrecio { Codigo = 1, Nombre = "Precio de prueba", Descripcion = "Prueba" };

            _mockTipoPrecioRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                                      .ReturnsAsync(tipoPrecio);

            var precioDePrueba = await _tipoPrecioController.UpsertBase(1);
            
            Assert.That(precioDePrueba, Is.InstanceOf<TipoPrecio>());
            
        }

        [Test]
        public async Task VerificarQueUpsertSirveParaCrearUnPrecio()
        {
            var precioDePrueba = await _tipoPrecioController.UpsertBase((int?)null);

            Assert.That(precioDePrueba, Is.InstanceOf<TipoPrecio>());
        }

        [Test]
        public async Task VerificarQueObtenerTodosSirve()
        {
            var preciosDePrueba = new List<TipoPrecio>
            {
                new() { Codigo = 1, Nombre = "Precio de prueba Ringo", Descripcion = "Prueba" },
                new() { Codigo = 2, Nombre = "Precio de prueba 2", Descripcion = "Prueba 2" }
            };

            _mockUnidadTrabajo.Setup(repo => repo.TipoPrecio.ObtenerTodos(
                It.IsAny<Expression<Func<TipoPrecio, bool>>>(),
                It.IsAny<Func<IQueryable<TipoPrecio>, IOrderedQueryable<TipoPrecio>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
            )).ReturnsAsync(preciosDePrueba);

            var precios = await _tipoPrecioController.ObtenerTodosBase();
            
            Assert.That(precios.Count(), Is.EqualTo(2));

        }

        [Test]
        public async Task VerificarQueDeleteBaseFunciona()
        {
            _mockTipoPrecioRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                                      .ReturnsAsync(new TipoPrecio { Codigo = 1, Nombre = "Precio de prueba", Descripcion = "Prueba" });

            Console.WriteLine("Se va a eliminar el precio");
            var resultado = await _tipoPrecioController.DeleteBase(1);

            Assert.That(resultado, Is.True);
        }

        
    }
}