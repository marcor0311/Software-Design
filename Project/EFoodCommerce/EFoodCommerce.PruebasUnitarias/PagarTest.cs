using System.Linq.Expressions;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.EFoodCommerce.Controllers;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.ViewModels;
using Moq;

namespace EFoodCommerce.PruebasUnitarias
{
    public class PagarTest
    {
        private Mock<IUnidadTrabajo> _unidadTrabajo;
        private Mock<IProcesadorPagoRepositorio> _procesadorPagoRepositorio;
        private Mock<ITarjetaProcesadorPagoRepositorio> _tarjetaProcesadorPagoRepositorio;
        private Mock<ITiqueteDescuentoRepositorio> _tiqueteDescuentoRepositorio;
        private PagarController _pagarController;

        [SetUp]
        public void Setup()
        {
            _unidadTrabajo = new Mock<IUnidadTrabajo>();
            _procesadorPagoRepositorio = new Mock<IProcesadorPagoRepositorio>();
            _tarjetaProcesadorPagoRepositorio = new Mock<ITarjetaProcesadorPagoRepositorio>();
            _tiqueteDescuentoRepositorio = new Mock<ITiqueteDescuentoRepositorio>();

            _unidadTrabajo.Setup(u => u.ProcesadorPago).Returns(_procesadorPagoRepositorio.Object);
            _unidadTrabajo.Setup(u => u.TarjetaProcesadorPago).Returns(_tarjetaProcesadorPagoRepositorio.Object);
            _unidadTrabajo.Setup(u => u.TiqueteDescuento).Returns(_tiqueteDescuentoRepositorio.Object);
            
            _pagarController = new PagarController(_unidadTrabajo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _unidadTrabajo = null!;
            _procesadorPagoRepositorio = null!;
            _tarjetaProcesadorPagoRepositorio = null!;
            _tiqueteDescuentoRepositorio = null!;
            _pagarController.Dispose();
        }

        [Test]
        public void TestMetodoPagoBase()
        {
            var procesadores = new[]
            {
                new ProcesadorPago
                {
                    Codigo = 1,
                    NombreProcesador = "Procesador 1"
                }
            };

            _unidadTrabajo.Setup(u => u.ProcesadorPago.ObtenerTodos(
                It.IsAny<Expression<Func<ProcesadorPago, bool>>>(),
                It.IsAny<Func<IQueryable<ProcesadorPago>, IOrderedQueryable<ProcesadorPago>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
            )).ReturnsAsync(procesadores);

            var result = _pagarController.MetodoPagoBase().Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.procesadores, Is.EqualTo(procesadores));
        }

        [Test]
        public void TestPagarTarjetaBase()
        {
            var tarjetas = new[]
            {
                new Tarjeta
                {
                    Codigo = 1,
                    Nombre = "Tarjeta 1"
                }
            };

            _unidadTrabajo.Setup(u => u.TarjetaProcesadorPago.ObtenerTarjetasPorProcesadorPagoNombre(It.IsAny<string>())).ReturnsAsync(tarjetas);

            var result = _pagarController.PagarTarjetaBase("Procesador 1").Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TarjetasLista, Is.EqualTo(tarjetas));
        }

        [Test]
        public void TestVerificarCodigoDescuento()
        {
            string codigoDescuento = "DESCUENTO";
            var descuento = new TiqueteDescuento
            {
                Codigo = 1,
                Descuento = 10,
                Disponibles = 1
            };

            _unidadTrabajo.Setup(u => u.TiqueteDescuento.Obtener_String_Codigo(codigoDescuento)).ReturnsAsync(descuento);

            var result = _pagarController.VerificarCodigoDescuento(codigoDescuento).Result;

            Assert.That(result, Is.True);

            codigoDescuento = null!;

            result = _pagarController.VerificarCodigoDescuento(codigoDescuento).Result;

            Assert.That(result, Is.False);

            codigoDescuento = "DESCUENTO";
            descuento.Disponibles = 0;

            result = _pagarController.VerificarCodigoDescuento(codigoDescuento).Result;

            Assert.That(result, Is.False);
        }

        [Test]
        public void TestObtenerPorcentajeDescuento()
        {
            string codigoDescuento = "DESCUENTO";
            var descuento = new TiqueteDescuento
            {
                Codigo = 1,
                Descuento = 10,
                Disponibles = 1
            };

            _unidadTrabajo.Setup(u => u.TiqueteDescuento.Obtener_String_Codigo(codigoDescuento)).ReturnsAsync(descuento);

            var result = _pagarController.ObtenerPorcentajeDescuento(codigoDescuento).Result;

            Assert.That(result, Is.EqualTo(descuento.Descuento));

            codigoDescuento = null!;

            result = _pagarController.ObtenerPorcentajeDescuento(codigoDescuento).Result;

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void AgregarPedido()
        {
            var total = 500;
            string codigoDescuento = "DESCUENTO";

            var descuento = new TiqueteDescuento
            {
                Codigo = 1,
                Descuento = 10,
                Disponibles = 1
            };

            CarroCompraVM carroCompraVM = new()
            {
                CarroCompra = new CarroCompra
                {
                    ProductoCodigo = 1
                }
            };

            _unidadTrabajo.Setup(u => u.TiqueteDescuento.Obtener_String_Codigo(codigoDescuento)).ReturnsAsync(descuento);
            _unidadTrabajo.Setup(u => u.TiqueteDescuento.Obtener_String_Codigo("N/A")).ReturnsAsync(descuento);
            _unidadTrabajo.Setup(u => u.Pedido.Agregar(It.IsAny<Pedido>())).Callback<Pedido>(p => {p.Codigo = 1; carroCompraVM.Pedido = p;});

            var result = _pagarController.AgregarPedido(total, codigoDescuento).Result;

            Assert.That(result, Is.EqualTo(1));
            Assert.That(carroCompraVM.Pedido, Is.Not.Null);
            Assert.That(carroCompraVM.Pedido.CodigoDescuento, Is.EqualTo(descuento.Codigo));
            Assert.That(descuento.Disponibles, Is.EqualTo(0));
            Assert.That(carroCompraVM.Pedido.Monto, Is.EqualTo(total));

            total = 1000;
            codigoDescuento = null!;

            result = _pagarController.AgregarPedido(total, codigoDescuento).Result;

            Assert.That(result, Is.EqualTo(1));
            Assert.That(carroCompraVM.Pedido, Is.Not.Null);
            Assert.That(carroCompraVM.Pedido.CodigoDescuento, Is.EqualTo(descuento.Codigo));
            Assert.That(descuento.Disponibles, Is.EqualTo(0));
            Assert.That(carroCompraVM.Pedido.Monto, Is.EqualTo(total));
        }
    }
}