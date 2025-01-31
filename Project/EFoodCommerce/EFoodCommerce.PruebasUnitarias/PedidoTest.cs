using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using Moq;
using System.Linq.Expressions;

namespace EFoodCommerce.PruebasUnitarias
{
    public class PedidoTest
    {
        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<IPedidoRepositorio> _mockPedidoRepositorio;
        private PedidoController _pedidoController;

        [SetUp]
        public void SetUp()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockPedidoRepositorio = new Mock<IPedidoRepositorio>();

            _mockUnidadTrabajo.Setup(u => u.Pedido).Returns(_mockPedidoRepositorio.Object);
            
            _pedidoController = new PedidoController(_mockUnidadTrabajo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockPedidoRepositorio = null!;
            _pedidoController.Dispose();
        }

        [Test]
        public async Task VerificarQueObtenerTodosPedidoSirve()
        {
            var t1 = new TiqueteDescuento();
            var t2 = new TiqueteDescuento();
            var pedidosDePrueba = new List<Pedido>
            {
                
                new() {Codigo = 1, Monto = 1000, FechaHora = DateTime.Now, Estado = "Activo 1", CodigoDescuento = 0, TiqueteDescuento = t1},
                new() {Codigo = 2, Monto = 2000, FechaHora = DateTime.Now, Estado = "Activo 2", CodigoDescuento = 0, TiqueteDescuento = t2}

            };

            _mockUnidadTrabajo.Setup(repo => repo.Pedido.ObtenerTodos(
                It.IsAny<Expression<Func<Pedido, bool>>>(),
                It.IsAny<Func<IQueryable<Pedido>, IOrderedQueryable<Pedido>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
             )).ReturnsAsync(pedidosDePrueba);

            var pedidos = await _pedidoController.ObtenerTodosBase();

            Assert.That(pedidos.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task VerificarQueEliminarPedidoSirve()
        {
            var t1 = new TiqueteDescuento();

            _mockPedidoRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                .ReturnsAsync(new Pedido { Codigo = 1, Monto = 1000, Estado = "Activo1", FechaHora = DateTime.Now, CodigoDescuento = 0, TiqueteDescuento = t1 });

            var resultado = await _pedidoController.DeleteBase(1);

            Assert.That(resultado, Is.True);
        }

    }
}
