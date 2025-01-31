using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class PrecioProductoPedidoRepositorio : Repositorio<PrecioProductoPedido>, IPrecioProductoPedidoRepositorio
    {

        private readonly ApplicationDbContext _db;

        public PrecioProductoPedidoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
