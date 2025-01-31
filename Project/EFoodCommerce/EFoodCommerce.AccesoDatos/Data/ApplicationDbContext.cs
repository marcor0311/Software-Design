
using System.Reflection;
using EFoodCommerce.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EFoodCommerce.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IUsuarioActual UsuarioActual;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUsuarioActual UsuarioActual)
            : base(options)
        {
            this.UsuarioActual = UsuarioActual ?? throw new ArgumentNullException(nameof(UsuarioActual));
        }

        public DbSet<Bitacora> Bitacoras { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<LineaComida> LineaComidas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PrecioProducto> PrecioProductos { get; set; }
        public DbSet<PrecioProductoPedido> PrecioProductoPedidos { get; set; }
        public DbSet<ProcesadorPago> ProcesadorPagos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<TarjetaProcesadorPago> TarjetaProcesadorPagos { get; set; }
        public DbSet<TipoPrecio> TipoPrecios { get; set; }
        public DbSet<TiqueteDescuento> TiqueteDescuentos { get; set; }
        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }
        public DbSet<CarroCompra> CarroCompra { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           
            ProcesarSalvado();
            return base.SaveChangesAsync(cancellationToken);

        }

        private void ProcesarSalvadoDeTipoDeCambio(List<Bitacora> bitacoras, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> entityEntries, string message, bool saveChanges) {
            foreach (var item in entityEntries)
            {
                var entidad = item.Entity;
                if (saveChanges)
                {
                    base.SaveChanges();
                }
                Bitacora b = new()
                {
                    FechaHora = DateTime.Now,
                    NombreUsuario = UsuarioActual.ObtenerUsuarioActual() ?? "N/A",
                    Descripcion = message + " " + item.Entity.GetType().Name,
                    CodigoRegistro = 0
                };

                if (item.Entity.GetType().Name != "UsuarioAplicacion" && !item.Entity.GetType().Name.StartsWith("IdentityUserRole"))
                {
                    Console.WriteLine(item.Entity.GetType().Name);
                    var primaryKey = item.Metadata.FindPrimaryKey();
                    if (primaryKey != null) {
                        var primaryKeyValue = item.Properties
                                    .Where(p => primaryKey.Properties.Contains(p.Metadata))
                                    .Select(p => p.CurrentValue)
                                    .FirstOrDefault();
                        int codigoRegistro = Convert.ToInt32(primaryKeyValue);
                        b.CodigoRegistro = codigoRegistro;
                    }
                }

                bitacoras.Add(b);
            }
        }
        private void ProcesarSalvado()
        {
            var horaActual = DateTime.Now;
            List<Bitacora> bitacorasToAdd = [];

            ProcesarSalvadoDeTipoDeCambio(bitacorasToAdd, ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added), "Nuevo dato ingresado en la tabla", true);
            ProcesarSalvadoDeTipoDeCambio(bitacorasToAdd, ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified), "Dato modificado en la tabla", false);
            ProcesarSalvadoDeTipoDeCambio(bitacorasToAdd, ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted), "Dato eliminado en la tabla", false);

            foreach (var bitacora in bitacorasToAdd)
            {
                Bitacoras.Add(bitacora);
            }
        }
    }
}

