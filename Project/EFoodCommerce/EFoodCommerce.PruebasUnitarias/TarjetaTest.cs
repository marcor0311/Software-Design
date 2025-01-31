using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using Moq;
using System.Linq.Expressions;

namespace EFoodCommerce.PruebasUnitarias
{
    public class TarjetaTest
    {

        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<ITarjetaRepositorio> _mockTarjetaRepositorio;
        private TarjetaController _tarjetaController;

        [SetUp]
        public void SetUp()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockTarjetaRepositorio = new Mock<ITarjetaRepositorio>();

            _mockUnidadTrabajo.Setup(u => u.Tarjeta).Returns(_mockTarjetaRepositorio.Object);

            _tarjetaController = new TarjetaController(_mockUnidadTrabajo.Object);

        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockTarjetaRepositorio = null!;
            _tarjetaController.Dispose();
        }

        [Test]
        public async Task VerificarQueObtenerTodosTarjetaSirve()
        {
            var tarjetasDePrueba = new List<Tarjeta>
            {

                new() {Codigo = 1, Nombre = "Visa", Descripcion = "Tarjeta de crédito"},
                new() {Codigo = 2, Nombre = "MasterCard", Descripcion = "Tarjeta de crédito"}

            };

            _mockUnidadTrabajo.Setup(repo => repo.Tarjeta.ObtenerTodos(

                It.IsAny<Expression<Func<Tarjeta, bool>>>(),
                It.IsAny<Func<IQueryable<Tarjeta>, IOrderedQueryable<Tarjeta>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
             )).ReturnsAsync(tarjetasDePrueba);

            var tarjetas = await _tarjetaController.ObtenerTodosBase();

            Assert.That(tarjetas.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task VerificarQueEliminarTarjetaSirve()
        {
            _mockTarjetaRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                .ReturnsAsync(new Tarjeta {Codigo = 1, Nombre = "Visa", Descripcion = "Tarjeta de crédito"});

            var resultado = await _tarjetaController.DeleteBase(1);

            Assert.That(resultado, Is.True);
        }

        [Test]
        public async Task VerificarQueUnaTarjetaMockSiDevuelveValoresEnUpsert()
        {
            var tarjeta = new Tarjeta {Codigo = 1, Nombre = "Visa", Descripcion = "Tarjeta de crédito"};

            _mockTarjetaRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                                      .ReturnsAsync(tarjeta);

            var lineaDePrueba = await _tarjetaController.UpsertBase(1);

            Assert.That(lineaDePrueba, Is.InstanceOf<Tarjeta>());

        }

        [Test]
        public async Task VerificarQueUpsertSirveParaCrearUnaTarjeta()
        {
            var tarjetasDePrueba = await _tarjetaController.UpsertBase((int?)null);

            Assert.That(tarjetasDePrueba, Is.InstanceOf<Tarjeta>());

        }
    }
}