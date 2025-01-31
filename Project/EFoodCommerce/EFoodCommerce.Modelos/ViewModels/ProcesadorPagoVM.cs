namespace EFoodCommerce.Modelos.ViewModels
{
    public class ProcesadorPagoVM
    {
        public ProcesadorPago ProcesadorPago { get; set; } = null!;
        public IEnumerable<Tarjeta>? TarjetasLista { get; set; }

        public IEnumerable<ProcesadorPago>? procesadores { get; set; }
        public IEnumerable<int>? TarjetasSeleccionadasLista { get; set; } = null!;
    }
}
