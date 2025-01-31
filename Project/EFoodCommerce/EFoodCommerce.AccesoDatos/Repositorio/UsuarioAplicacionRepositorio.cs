using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class UsuarioAplicacionRepositorio : Repositorio<UsuarioAplicacion>, IUsuarioAplicacionRepositorio
    {

        private readonly ApplicationDbContext _db;

        public UsuarioAplicacionRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<UsuarioAplicacion?> ObtenerFake(string id)
        {
            return await _db.Set<UsuarioAplicacion>().FindAsync(id);
        }

        public bool ContieneFake(string id)
        {
            return ObtenerFake(id) != null;
        }
    }
}
