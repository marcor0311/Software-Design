using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using Microsoft.EntityFrameworkCore;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class TarjetaProcesadorPagoRepositorio : Repositorio<TarjetaProcesadorPago>, ITarjetaProcesadorPagoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public TarjetaProcesadorPagoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void EliminarPorProcesadorPago(int procesadorPagoId) {
            var listaTarjetas = _db.TarjetaProcesadorPagos.Where(i => i.CodigoProcesadorPago == procesadorPagoId);
            _db.TarjetaProcesadorPagos.RemoveRange(listaTarjetas);
        }

        public void AgregarPorProcesadorPago(int procesadorPagoId, IEnumerable<int> tarjetas) {
            foreach (var tarjeta in tarjetas)
            {
                _db.TarjetaProcesadorPagos.Add(new TarjetaProcesadorPago
                {
                    CodigoProcesadorPago = procesadorPagoId,
                    CodigoTarjeta = tarjeta
                });
            }
        }

        public IEnumerable<int> BuscarPorProcesadorPago(int? procesadorPagoId) {
            return _db.TarjetaProcesadorPagos.Where(i => i.CodigoProcesadorPago == procesadorPagoId).Select(i => i.CodigoTarjeta);
        }

        public async Task<IEnumerable<Tarjeta>> ObtenerTarjetasPorProcesadorPagoNombre(string? procesadorPagoNombre)
        {

            var procesador = await _db.ProcesadorPagos.FirstOrDefaultAsync(t => t.NombreProcesador == procesadorPagoNombre);
            if (procesador == null)
            {
                return [];
            }
            int codigo_procesador = procesador.Codigo;
            IEnumerable<int> lista_tarjetas_codigos = _db.TarjetaProcesadorPagos.Where(t => t.CodigoProcesadorPago == codigo_procesador).Select(i => i.CodigoTarjeta);
            IEnumerable<Tarjeta> tarjetas = [.. _db.Tarjetas.Where(t => lista_tarjetas_codigos.Contains(t.Codigo))];
            return tarjetas;


        }
    }
}
