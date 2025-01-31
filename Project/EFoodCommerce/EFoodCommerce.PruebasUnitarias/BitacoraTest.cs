using Moq;
using EFoodCommerce.Modelos;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using System.Linq.Expressions;

namespace EFoodCommerce.PruebasUnitarias
{
    public class BitacoraTest
    {
        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<IBitacoraRepositorio> _mockBitacoraRepositorio;
        private BitacoraController _bitacoraController;

        [SetUp]
        public void SetUp()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockBitacoraRepositorio = new Mock<IBitacoraRepositorio>();

            _mockUnidadTrabajo.Setup(u => u.Bitacora).Returns(_mockBitacoraRepositorio.Object);
            
            _bitacoraController = new BitacoraController(_mockUnidadTrabajo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockBitacoraRepositorio = null!;
            _bitacoraController.Dispose();
        }

        [Test]

        public async Task VerificarQueObtenerTodosBitacoraSirve()
        {
            var bitacorasDePrueba = new List<Bitacora>
            {

                new() {Codigo = 1,NombreUsuario = "Max Chatysh", FechaHora = DateTime.Now, CodigoRegistro = 1, Descripcion = "Dato de prueba 1"},
                new() {Codigo = 2,NombreUsuario = "Charco Rodriguez", FechaHora = DateTime.Now, CodigoRegistro = 2, Descripcion = "Dato de prueba 2"}

            };

            _mockUnidadTrabajo.Setup(repo => repo.Bitacora.ObtenerTodos(
                It.IsAny<Expression<Func<Bitacora, bool>>>(),
                It.IsAny<Func<IQueryable<Bitacora>, IOrderedQueryable<Bitacora>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
             )).ReturnsAsync(bitacorasDePrueba);

            var bitacoras = await _bitacoraController.ObtenerTodosBase();

            Assert.That(bitacoras.Count(), Is.EqualTo(2));
        }
    }
}
