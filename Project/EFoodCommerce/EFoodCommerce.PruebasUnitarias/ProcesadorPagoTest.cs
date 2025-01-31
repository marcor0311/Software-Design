using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.ViewModels;
using Moq;
using System.Linq.Expressions;

namespace EFoodCommerce.PruebasUnitarias
{
    public class ProcesadorPagoTest
    {

        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<IProcesadorPagoRepositorio> _mockProcesadorPagoRepositorio;
        private Mock<ITarjetaProcesadorPagoRepositorio> _mockTarjetaProcesadorPagoRepositorio;
        private Mock<ITarjetaRepositorio> _mockTarjetaRepositorio;
        private ProcesadorPagoController _procesadorPagoController;

        [SetUp]
        public void SetUp()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockProcesadorPagoRepositorio = new Mock<IProcesadorPagoRepositorio>();
            _mockTarjetaProcesadorPagoRepositorio = new Mock<ITarjetaProcesadorPagoRepositorio>();
            _mockTarjetaRepositorio = new Mock<ITarjetaRepositorio>();

            _mockUnidadTrabajo.Setup(u => u.ProcesadorPago).Returns(_mockProcesadorPagoRepositorio.Object);
            _mockUnidadTrabajo.Setup(u => u.TarjetaProcesadorPago).Returns(_mockTarjetaProcesadorPagoRepositorio.Object);
            _mockUnidadTrabajo.Setup(u => u.Tarjeta).Returns(_mockTarjetaRepositorio.Object);
            
            _procesadorPagoController = new ProcesadorPagoController(_mockUnidadTrabajo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockProcesadorPagoRepositorio = null!;
            _procesadorPagoController.Dispose();
        }

        [Test]
        public async Task VerificarQueObtenerTodosProcesadorPagoSirve()
        {

            var procesadoresPagoDePrueba = new List<ProcesadorPago>
            {

                new() {Codigo = 1, NombreProcesador = "Paypal", Tipo = "debito/credito", NombreOpcionPago = "Tarjeta de Crédito", Estado = true, Verificacion = true, Metodo = "Tarjeta de Crédito"},
                new() {Codigo = 2, NombreProcesador = "Stripe", Tipo = "debito/credito", NombreOpcionPago = "Tarjeta de Crédito", Estado = true, Verificacion = true, Metodo = "Tarjeta de Crédito"}

            };

            _mockUnidadTrabajo.Setup(repo => repo.ProcesadorPago.ObtenerTodos(

                It.IsAny<Expression<Func<ProcesadorPago, bool>>>(),
                It.IsAny<Func<IQueryable<ProcesadorPago>, IOrderedQueryable<ProcesadorPago>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
             )).ReturnsAsync(procesadoresPagoDePrueba);

            var procesadoresPago = await _procesadorPagoController.ObtenerTodosBase();
            Assert.That(procesadoresPago.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task VerificarQueEliminarProcesadorPagoSirve()
        {
            _mockProcesadorPagoRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                .ReturnsAsync(new ProcesadorPago {Codigo = 1, NombreProcesador = "Paypal", Tipo = "debito/credito", NombreOpcionPago = "Tarjeta de Crédito", Estado = true, Verificacion = true, Metodo = "Tarjeta de Crédito"});

            var resultado = await _procesadorPagoController.DeleteBase(1);

            Assert.That(resultado, Is.True);
        }

        [Test]
        public async Task VerificarQueUnaProcesadorPagoMockSiDevuelveValoresEnUpsert()
        {

            var ProcesadorPago = new ProcesadorPago {Codigo = 1, NombreProcesador = "Paypal", Tipo = "debito/credito", NombreOpcionPago = "Tarjeta de Crédito", Estado = true, Verificacion = true, Metodo = "Tarjeta de Crédito"};
            var Tarjetas = new List<Tarjeta>
            {
                new Tarjeta {Codigo = 1, Nombre = "Visa", Descripcion = "Tarjeta de crédito"},
                new Tarjeta {Codigo = 2, Nombre = "MasterCard", Descripcion = "Tarjeta de crédito"},
                new Tarjeta {Codigo = 3, Nombre = "Regex", Descripcion = "Tarjeta de crédito"}
            };
            var TarjetasSeleccionadasLista = new List<int>
            {
                1, 2
            };

            _mockProcesadorPagoRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                                      .ReturnsAsync(ProcesadorPago);
            _mockUnidadTrabajo.Setup(repo => repo.Tarjeta.ObtenerTodos(
                It.IsAny<Expression<Func<Tarjeta, bool>>>(),
                It.IsAny<Func<IQueryable<Tarjeta>, IOrderedQueryable<Tarjeta>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
            )).ReturnsAsync(Tarjetas);
            _mockTarjetaProcesadorPagoRepositorio.Setup(repo => repo.BuscarPorProcesadorPago(It.IsAny<int>()))
                .Returns(TarjetasSeleccionadasLista);

            var procesadoresPago = await _procesadorPagoController.UpsertBase(1);

            Assert.That(procesadoresPago, Is.InstanceOf<ProcesadorPagoVM>());

        }

        [Test]
        public async Task VerificarQueUpsertSirveParaCrearUnaLineaDeComida()
        {
            var Tarjetas = new List<Tarjeta>
            {
                new() {Codigo = 1, Nombre = "Visa", Descripcion = "Tarjeta de crédito"},
                new() {Codigo = 2, Nombre = "MasterCard", Descripcion = "Tarjeta de crédito"},
                new() {Codigo = 3, Nombre = "Regex", Descripcion = "Tarjeta de crédito"}
            };
            var TarjetasSeleccionadasLista = new List<int>
            {
                1, 2
            };

            _mockUnidadTrabajo.Setup(repo => repo.Tarjeta.ObtenerTodos(
                It.IsAny<Expression<Func<Tarjeta, bool>>>(),
                It.IsAny<Func<IQueryable<Tarjeta>, IOrderedQueryable<Tarjeta>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
            )).ReturnsAsync(Tarjetas);
            _mockTarjetaProcesadorPagoRepositorio.Setup(repo => repo.BuscarPorProcesadorPago(It.IsAny<int>()))
                .Returns(TarjetasSeleccionadasLista);
            
            var procesadoresPago = await _procesadorPagoController.UpsertBase((int?)null);

            Assert.That(procesadoresPago, Is.InstanceOf<ProcesadorPagoVM>());

        }
    }
}