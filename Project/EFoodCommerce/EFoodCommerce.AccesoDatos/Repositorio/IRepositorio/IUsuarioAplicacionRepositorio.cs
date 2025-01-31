using EFoodCommerce.Modelos;

namespace EFoodCommerce.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUsuarioAplicacionRepositorio : IRepositorio<UsuarioAplicacion>
    {

        Task<UsuarioAplicacion?> ObtenerFake(string id);

        bool ContieneFake(string id);

    }
}