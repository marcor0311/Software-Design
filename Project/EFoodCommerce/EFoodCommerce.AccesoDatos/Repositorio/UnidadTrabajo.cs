using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
	public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;

        public IBitacoraRepositorio Bitacora { get; private set; }
        public IErrorRepositorio Error { get; private set; }
        public ILineaComidaRepositorio LineaComida { get; private set; }
        public IPedidoRepositorio Pedido { get; private set; }
        public IPrecioProductoRepositorio PrecioProducto { get; private set; }
        public IPrecioProductoPedidoRepositorio PrecioProductoPedido { get; private set; }
        public IProcesadorPagoRepositorio ProcesadorPago { get; private set; }
        public IProductoRepositorio Producto { get; private set; }
        public ITarjetaRepositorio Tarjeta { get; private set; }
        public ITarjetaProcesadorPagoRepositorio TarjetaProcesadorPago { get; private set; }
        public ITipoPrecioRepositorio TipoPrecio { get; private set; }
        public ITiqueteDescuentoRepositorio TiqueteDescuento { get; private set; }
        public ICarroCompraRepositorio CarroCompra {  get; private set; }

        public IUsuarioAplicacionRepositorio UsuarioAplicacion { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db, IHttpContextAccessor sessionAccessor)
		{
            _db = db;

            Bitacora = new BitacoraRepositorio(_db);
            Error = new ErrorRepositorio(_db);
            LineaComida = new LineaComidaRepositorio(_db);
            Pedido = new PedidoRepositorio(_db);
            PrecioProducto = new PrecioProductoRepositorio(_db);
            PrecioProductoPedido = new PrecioProductoPedidoRepositorio(_db);
            ProcesadorPago = new ProcesadorPagoRepositorio(_db);
            Producto = new ProductoRepositorio(_db);
            Tarjeta = new TarjetaRepositorio(_db);
            TarjetaProcesadorPago = new TarjetaProcesadorPagoRepositorio(_db);
            TipoPrecio = new TipoPrecioRepositorio(_db);
            TiqueteDescuento = new TiqueteDescuentoRepositorio(_db);
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
            CarroCompra = new CarroCompraRepositorio(_db, sessionAccessor);
        }


        public void DesacoplarEntidad<T>(T entidad) where T : class
        {
            _db.Entry(entidad).State = EntityState.Detached;
        }



        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }

        public void Deshacer()
        {
            foreach (var entry in _db.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                    case EntityState.Modified:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}

