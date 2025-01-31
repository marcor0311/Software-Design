using EFoodCommerce.AccesoDatos.Repositorio;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EFoodCommerce.Areas.EFoodCommerce.Controllers
{
    [Area("EFoodCommerce")]
    public class PagarController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public PagarController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ProcesadorPagoVM> MetodoPagoBase()
        {
            var p = await _unidadTrabajo.ProcesadorPago.ObtenerTodos();
            ProcesadorPagoVM? procesadoresPago = new()
            {
                procesadores = p
            };
            return procesadoresPago;
        }

        public async Task<IActionResult> MetodoPago()
        {
            var procesadoresPago = await MetodoPagoBase();
            return View(procesadoresPago);
        }

        public async Task<ProcesadorPagoVM> PagarTarjetaBase(string procesador_pago_nombre) {
            var t = await _unidadTrabajo.TarjetaProcesadorPago.ObtenerTarjetasPorProcesadorPagoNombre(procesador_pago_nombre);
            ProcesadorPagoVM tarjetas = new(){
                TarjetasLista = t
            };
            return tarjetas;
        }

        public async Task<IActionResult> PagarTarjeta(string procesador_pago_nombre)
        {
            var tarjetas = await PagarTarjetaBase(procesador_pago_nombre);
            return View(tarjetas);
        }

        public async Task<bool> VerificarCodigoDescuento(string codigoDescuento)
        {
            var descuento = await _unidadTrabajo.TiqueteDescuento.Obtener_String_Codigo(codigoDescuento);

            if (descuento != null && descuento.Disponibles > 0) {

                return true;
            }
            return false;
        }

        public async Task<int> ObtenerPorcentajeDescuento(string codigoDescuento)
        {
            if(codigoDescuento == null)
            {
                return 0;
            }
            else
            {
                var descuento = await _unidadTrabajo.TiqueteDescuento.Obtener_String_Codigo(codigoDescuento);
                if (descuento == null) return 0;
                return descuento.Descuento;

            }
        }

        public async Task<int> AgregarPedido(int total, string codigo_descuento)
        {

            Pedido p = new()
            {
                FechaHora = DateTime.Now,
                Monto = total,
                Estado = "En curso"
            };

            if (codigo_descuento == null)
            {
               codigo_descuento = "N/A";
               var d = (await _unidadTrabajo.TiqueteDescuento.Obtener_String_Codigo(codigo_descuento))!;
               p.CodigoDescuento = d.Codigo;
               p.TiqueteDescuento = d;
            } else
            {

                var descuento = (await _unidadTrabajo.TiqueteDescuento.Obtener_String_Codigo(codigo_descuento))!;
                p.CodigoDescuento = descuento.Codigo;
                p.TiqueteDescuento = descuento;
                descuento.Disponibles--;

                }
               
             await _unidadTrabajo.Pedido.Agregar(p);
             await _unidadTrabajo.Guardar();
             return p.Codigo;

        }

        public IActionResult ChequePagar()
        {
            return View();
        }

        public IActionResult Confirmacion()
        {
            return View();
        }

    }
}
