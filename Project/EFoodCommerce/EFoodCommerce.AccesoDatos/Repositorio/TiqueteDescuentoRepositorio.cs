using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using Microsoft.EntityFrameworkCore;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class TiqueteDescuentoRepositorio : Repositorio<TiqueteDescuento>, ITiqueteDescuentoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public TiqueteDescuentoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<TiqueteDescuento> ObtenerTiquetesDisponibles()
        {
            return [.. _db.TiqueteDescuentos.Where(t => t.Disponibles > 0)];
        }

        public async Task<TiqueteDescuento?> Obtener_String_Codigo(string codigo)
        {
            return await dbSet.FirstOrDefaultAsync(t => t.StringCodigo == codigo);
        }

    }
}
