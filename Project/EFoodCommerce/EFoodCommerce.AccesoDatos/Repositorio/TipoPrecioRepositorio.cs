using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class TipoPrecioRepositorio : Repositorio<TipoPrecio>, ITipoPrecioRepositorio
    {

        private readonly ApplicationDbContext _db;

        public TipoPrecioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
