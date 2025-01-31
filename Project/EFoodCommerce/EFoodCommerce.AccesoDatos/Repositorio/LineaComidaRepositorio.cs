using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class LineaComidaRepositorio : Repositorio<LineaComida>, ILineaComidaRepositorio
    {

        private readonly ApplicationDbContext _db;

        public LineaComidaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> ObtenerLineaComidaDropdownLista()
        {
            return _db.LineaComidas.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Codigo.ToString()
            });
        }
    }
}
