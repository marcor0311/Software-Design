using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class PrecioProductoRepositorio : Repositorio<PrecioProducto> , IPrecioProductoRepositorio
    {

        private readonly ApplicationDbContext _db;

        public PrecioProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PrecioProducto>> ObtenerPreciosPorCodigoProductoAsync(int codigoProducto)
        {
            Expression<Func<PrecioProducto, bool>> filtro = pp => pp.CodigoProducto == codigoProducto;
            string incluirPropiedades = "Producto,TipoPrecio";
            return await ObtenerTodos(filtro, null, incluirPropiedades, true);
        }


        public IEnumerable<SelectListItem> ObtenerTipoPrecioDropdownLista()
        {
            return _db.TipoPrecios.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Codigo.ToString()
            });
        }

        public async Task<IEnumerable<SelectListItem>> ObtenerPreciosPorCodigoProductoAsyncDropdownLista(int codigoProducto) {
            IEnumerable<PrecioProducto> precios = await ObtenerPreciosPorCodigoProductoAsync(codigoProducto);
            return precios.Select(c => new SelectListItem
            {
                Text = c.TipoPrecio?.Nombre + " - $" + c.Precio,
                Value = c.Codigo.ToString()
            });
        }
    }
}
