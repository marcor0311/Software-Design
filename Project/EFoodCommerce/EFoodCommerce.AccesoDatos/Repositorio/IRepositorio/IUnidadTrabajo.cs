
namespace EFoodCommerce.AccesoDatos.Repositorio.IRepositorio
{
	public interface IUnidadTrabajo
	{

        IBitacoraRepositorio Bitacora { get; }
        IErrorRepositorio Error { get; }
        ILineaComidaRepositorio LineaComida { get; }
        IPedidoRepositorio Pedido { get; }
        IPrecioProductoRepositorio PrecioProducto { get; }
        IPrecioProductoPedidoRepositorio PrecioProductoPedido { get; }
        IProcesadorPagoRepositorio ProcesadorPago { get; }
        IProductoRepositorio Producto { get; }
        ITarjetaRepositorio Tarjeta { get; }
        ITarjetaProcesadorPagoRepositorio TarjetaProcesadorPago { get; }
        ITipoPrecioRepositorio TipoPrecio { get; }
        ITiqueteDescuentoRepositorio TiqueteDescuento { get;}
        ICarroCompraRepositorio CarroCompra { get; }

        IUsuarioAplicacionRepositorio UsuarioAplicacion { get; }

        Task Guardar();
        
        void Deshacer();

        void DesacoplarEntidad<T>(T entidad) where T : class;

    }


}

