using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class ErrorRepositorio : Repositorio<Error>, IErrorRepositorio
    {

        private readonly ApplicationDbContext _db;

        public ErrorRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
