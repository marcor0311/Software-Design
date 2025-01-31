using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class TarjetaRepositorio : Repositorio<Tarjeta>, ITarjetaRepositorio
    {

        private readonly ApplicationDbContext _db;

        public TarjetaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
