using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class BitacoraRepositorio : Repositorio<Bitacora>, IBitacoraRepositorio
    {

        private readonly ApplicationDbContext _db;

        public BitacoraRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
