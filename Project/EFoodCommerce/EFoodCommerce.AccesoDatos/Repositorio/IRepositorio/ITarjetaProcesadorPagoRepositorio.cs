using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFoodCommerce.AccesoDatos.Repositorio.IRepositorio
{
    public interface ITarjetaProcesadorPagoRepositorio : IRepositorio<TarjetaProcesadorPago>
    {
        void EliminarPorProcesadorPago(int procesadorPagoId);
        void AgregarPorProcesadorPago(int procesadorPagoId, IEnumerable<int> tarjetas);

        IEnumerable<int> BuscarPorProcesadorPago(int? procesadorPagoId);
        Task<IEnumerable<Tarjeta>> ObtenerTarjetasPorProcesadorPagoNombre(string? procesadorPagoNombre);
    }
}