using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Areas.Admin.Controllers;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Linq.Expressions;

namespace EFoodCommerce.PruebasUnitarias
{
    public class UsuarioAplicacionTest
    {
        private Mock<IUnidadTrabajo> _mockUnidadTrabajo;
        private Mock<IUsuarioAplicacionRepositorio> _mockUsuarioAplicacionRepositorio;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private UsuarioAplicacionController _usuarioAplicacionController;

        [SetUp]
        public void Setup()
        {
            _mockUnidadTrabajo = new Mock<IUnidadTrabajo>();
            _mockUsuarioAplicacionRepositorio = new Mock<IUsuarioAplicacionRepositorio>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var store = new Mock<IUserStore<IdentityUser>>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            _usuarioAplicacionController = new UsuarioAplicacionController(_mockUnidadTrabajo.Object, _mockUserManager.Object, _mockHttpContextAccessor.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnidadTrabajo = null!;
            _mockUserManager = null!;
            _mockHttpContextAccessor = null!;
            _usuarioAplicacionController.Dispose();
        }

        [Test]
        public async Task VerificarQueObtenerTodosTraeLosUsuarios()
        {
            var usuariosDePrueba = new List<UsuarioAplicacion>
            {
                new UsuarioAplicacion { Id = "1", Nombre = "Usuario de prueba 1", Pregunta_Seguridad = "Prueba 1", Respuesta_Seguridad = "Respuesta 1", Role = "Rol de Prueba"},
                new UsuarioAplicacion { Id = "2", Nombre = "Usuario de prueba 2", Pregunta_Seguridad = "Prueba 2", Respuesta_Seguridad = "Respuesta 2", Role = "Rol de Prueba"},
                new UsuarioAplicacion { Id = "3", Nombre = "Usuario de prueba 3", Pregunta_Seguridad = "Prueba 3", Respuesta_Seguridad = "Respuesta 3", Role = "Rol de Prueba"}
            };

            _mockUnidadTrabajo.Setup(repo => repo.UsuarioAplicacion.ObtenerTodos(
                It.IsAny<Expression<Func<UsuarioAplicacion, bool>>>(),
                It.IsAny<Func<IQueryable<UsuarioAplicacion>, IOrderedQueryable<UsuarioAplicacion>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
            )).ReturnsAsync(usuariosDePrueba);

            var usuarios = await _usuarioAplicacionController.ObtenerTodosBase();
            foreach (var usuario in usuarios)
            {
                Console.WriteLine(usuario.Nombre);
            }
            Assert.That(usuarios.Count(), Is.EqualTo(3));
        }
        
        [Test]
        public async Task VerificarQueBloquearSirve()
        {
            _mockUnidadTrabajo.Setup(repo => repo.UsuarioAplicacion.ObtenerFake(It.IsAny<string>()))
                .ReturnsAsync(new UsuarioAplicacion { Id = "1", Nombre = "Usuario de prueba 1", Pregunta_Seguridad = "Prueba 1", Respuesta_Seguridad = "Respuesta 1", Role = "Rol de Prueba" });
            Console.WriteLine("Se va a bloquear el usuario");
            var resultado = await _usuarioAplicacionController.BloquearBesbloquearBase("1");
            var usuario = await _mockUnidadTrabajo.Object.UsuarioAplicacion.ObtenerFake("1");
            Assert.That(resultado, Is.True);
        }

        [Test]
        public async Task VerificarQueDesbloquearSirve()
        {
            _mockUnidadTrabajo.Setup(repo => repo.UsuarioAplicacion.ObtenerFake(It.IsAny<string>()))
                .ReturnsAsync(new UsuarioAplicacion { Id = "1", Nombre = "Usuario de prueba 1", Pregunta_Seguridad = "Prueba 1", Respuesta_Seguridad = "Respuesta 1", Role = "Rol de Prueba", LockoutEnd = DateTimeOffset.UtcNow.AddDays(10)});
            Console.WriteLine("Se va a desbloquear el usuario");
            var resultado = await _usuarioAplicacionController.BloquearBesbloquearBase("1");
            var usuario = await _mockUnidadTrabajo.Object.UsuarioAplicacion.ObtenerFake("1");
            
            Assert.That(usuario, Is.Not.Null);
            Assert.That(usuario.LockoutEnabled, Is.False);
            Assert.That(resultado, Is.True);
        }

        
        [Test]
        public async Task VerificarQueDeleteBaseFunciona()
        {
            _mockUnidadTrabajo.Setup(repo => repo.UsuarioAplicacion.ObtenerFake(It.IsAny<string>()))
                 .ReturnsAsync(new UsuarioAplicacion { Id = "1", Nombre = "Usuario de prueba 1", Pregunta_Seguridad = "Prueba 1", Respuesta_Seguridad = "Respuesta 1", Role = "Rol de Prueba", LockoutEnd = DateTimeOffset.UtcNow.AddDays(10) });
            Console.WriteLine("Se va a eliminar el Usuario");
            var resultado = await _usuarioAplicacionController.DeleteBase("1");
            Assert.That(resultado, Is.True);
        }
    }
}