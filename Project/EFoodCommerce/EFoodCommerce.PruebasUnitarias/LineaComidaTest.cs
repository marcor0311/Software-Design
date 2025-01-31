using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using Moq;
using System.Linq.Expressions;

namespace EFoodCommerce.PruebasUnitarias
{
    public class LineaComidaTest
    {

        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<ILineaComidaRepositorio> _mockLineaComidaRepositorio;
        private LineaComidaController _lineaComidaController;

        [SetUp]
        public void SetUp()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockLineaComidaRepositorio = new Mock<ILineaComidaRepositorio>();

            _mockUnidadTrabajo.Setup(u => u.LineaComida).Returns(_mockLineaComidaRepositorio.Object);
            
            _lineaComidaController = new LineaComidaController(_mockUnidadTrabajo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockLineaComidaRepositorio = null!;
            _lineaComidaController.Dispose();
        }

        [Test]
        public async Task VerificarQueObtenerTodosLineaComidaSirve()
        {

            var lineasComidasDePrueba = new List<LineaComida>
            {

                new() {Codigo = 1, Nombre = "Helados"},
                new() {Codigo = 2, Nombre = "Comida Rápida"}

            };

            _mockUnidadTrabajo.Setup(repo => repo.LineaComida.ObtenerTodos(
                It.IsAny<Expression<Func<LineaComida, bool>>>(),
                It.IsAny<Func<IQueryable<LineaComida>, IOrderedQueryable<LineaComida>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
             )).ReturnsAsync(lineasComidasDePrueba);

            var lineas_comida = await _lineaComidaController.ObtenerTodosBase();

            Assert.That(lineas_comida.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task VerificarQueEliminarLineaComidaSirve()
        {
            _mockLineaComidaRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                .ReturnsAsync(new LineaComida { Codigo = 1, Nombre = "Comida Rápida"});

            var resultado = await _lineaComidaController.DeleteBase(1);

            Assert.That(resultado, Is.True);
        }

        [Test]
        public async Task VerificarQueUnaLineaComidaMockSiDevuelveValoresEnUpsert()
        {
            var lineaComida = new LineaComida { Codigo = 1, Nombre = "Linea de Prueba"};

            _mockLineaComidaRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                                      .ReturnsAsync(lineaComida);

            var lineaDePrueba = await _lineaComidaController.UpsertBase(1);

            Assert.That(lineaDePrueba, Is.InstanceOf<LineaComida>());

        }

        [Test]
        public async Task VerificarQueUpsertSirveParaCrearUnaLineaDeComida()
        {
            var lineaDePrueba = await _lineaComidaController.UpsertBase((int?)null);

            Assert.That(lineaDePrueba, Is.InstanceOf<LineaComida>());
        }
    }
}
