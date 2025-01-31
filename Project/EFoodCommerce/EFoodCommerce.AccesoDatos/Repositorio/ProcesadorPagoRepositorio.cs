using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class ProcesadorPagoRepositorio : Repositorio<ProcesadorPago>, IProcesadorPagoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProcesadorPagoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> ObtenerTarjetasDropdownLista()
        {
            return _db.Tarjetas.Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Codigo.ToString()
                });
        }
    }
}
