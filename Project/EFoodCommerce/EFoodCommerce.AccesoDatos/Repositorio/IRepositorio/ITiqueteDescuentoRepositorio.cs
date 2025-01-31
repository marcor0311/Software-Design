using EFoodCommerce.Modelos;

namespace EFoodCommerce.AccesoDatos.Repositorio.IRepositorio
{
    public interface ITiqueteDescuentoRepositorio : IRepositorio<TiqueteDescuento>
    {

        IEnumerable<TiqueteDescuento> ObtenerTiquetesDisponibles();
        public Task<TiqueteDescuento?> Obtener_String_Codigo(string codigo);

    }
}
