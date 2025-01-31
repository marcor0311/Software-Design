using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using Moq;
using System.Linq.Expressions;

namespace EFoodCommerce.PruebasUnitarias
{
    public class TiqueteDescuentoTest
    {
        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<ITiqueteDescuentoRepositorio> _mockTiqueteDescuentoRepositorio;
        private TiqueteDescuentoController _tiqueteDescuentoController;

        [SetUp]
        public void Setup()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockTiqueteDescuentoRepositorio = new Mock<ITiqueteDescuentoRepositorio>();
            
            _mockUnidadTrabajo.Setup(u => u.TiqueteDescuento).Returns(_mockTiqueteDescuentoRepositorio.Object);

            _tiqueteDescuentoController = new TiqueteDescuentoController(_mockUnidadTrabajo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockTiqueteDescuentoRepositorio = null!;
            _tiqueteDescuentoController.Dispose();
        }

        [Test]
        public async Task VerificarQueUnTiqueteMockSiDevuelveValoresEnUpsert()
        {
            
            var tiqueteDescuento = new TiqueteDescuento { Codigo = 1, StringCodigo="PRUEBA", Nombre = "Tiquete de prueba", Descuento = 10, Disponibles = 10 };
            
            _mockTiqueteDescuentoRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                                      .ReturnsAsync(tiqueteDescuento);
                                      
            var tiqueteDePrueba = await _tiqueteDescuentoController.UpsertBase(1);

            Assert.That(tiqueteDePrueba, Is.Not.Null);
            Console.WriteLine(tiqueteDePrueba.StringCodigo);
            Assert.That(tiqueteDePrueba, Is.InstanceOf<TiqueteDescuento>());
        }
        
        [Test]
        public async Task VerificarQueUpsertSirveParaCrearUnTiquete()
        {
            var tiqueteDePrueba = await _tiqueteDescuentoController.UpsertBase((int?)null);

            Assert.That(tiqueteDePrueba, Is.InstanceOf<TiqueteDescuento>());  
        }
        
        [Test]
        public async Task VerificarQueObtenerTodosSirve()
        {
            var tiquetesDePrueba = new List<TiqueteDescuento>
            {
                new() { Codigo = 1, StringCodigo="PRUEBA", Nombre = "Tiquete de prueba", Descuento = 10, Disponibles = 10 },
                new() { Codigo = 2, StringCodigo="PRUEBA2", Nombre = "Tiquete de prueba 2", Descuento = 20, Disponibles = 20 }
            };

            _mockUnidadTrabajo.Setup(repo => repo.TiqueteDescuento.ObtenerTodos(
                It.IsAny<Expression<Func<TiqueteDescuento, bool>>>(),
                It.IsAny<Func<IQueryable<TiqueteDescuento>, IOrderedQueryable<TiqueteDescuento>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
            )).ReturnsAsync(tiquetesDePrueba);

            var tiquetes = await _tiqueteDescuentoController.ObtenerTodosBase();

            foreach (var tiquete in tiquetes)
            {
                Console.WriteLine(tiquete.Nombre);
            }
            Assert.That(tiquetes.Count(), Is.EqualTo(2));

        }
        
        [Test]
        public async Task VerificarQueDeleteBaseFunciona()
        {
            _mockTiqueteDescuentoRepositorio.Setup(repo => repo.Obtener(It.IsAny<int>()))
                                      .ReturnsAsync(new TiqueteDescuento { Codigo = 1, StringCodigo = "PRUEBA", Nombre = "Tiquete de prueba", Descuento = 10, Disponibles = 10 });

            Console.WriteLine("Se va a eliminar el precio");

            var resultado = await _tiqueteDescuentoController.DeleteBase(1);

            Assert.That(resultado, Is.True);
        }

        
    }
}