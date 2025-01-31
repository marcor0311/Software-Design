using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using Moq;
using System.Linq.Expressions;

namespace EFoodCommerce.PruebasUnitarias
{
    public class ErrorTest
    {
        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<IErrorRepositorio> _mockErrorRepositorio;
        private ErrorController _errorController;

        [SetUp]
        public void SetUp()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockErrorRepositorio = new Mock<IErrorRepositorio>();

            _mockUnidadTrabajo.Setup(u => u.Error).Returns(_mockErrorRepositorio.Object);
            
            _errorController = new ErrorController(_mockUnidadTrabajo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockErrorRepositorio = null!;
            _errorController.Dispose();
        }

        [Test]
        public async Task VerificarQueObtenerTodosErrorSirve()
        {
            var erroresDePrueba = new List<Error>
            {
                new() {Codigo = 1, FechaHora = DateTime.Now, Mensaje = "Error 1"},
                new() {Codigo = 2, FechaHora = DateTime.Now, Mensaje = "Error 2"}
            };

            _mockUnidadTrabajo.Setup(repo => repo.Error.ObtenerTodos(
                It.IsAny<Expression<Func<Error, bool>>>(),
                It.IsAny<Func<IQueryable<Error>, IOrderedQueryable<Error>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
             )).ReturnsAsync(erroresDePrueba);

            var errores = await _errorController.ObtenerTodosBase();

            Assert.That(errores.Count(), Is.EqualTo(2));
        }
    }
}
