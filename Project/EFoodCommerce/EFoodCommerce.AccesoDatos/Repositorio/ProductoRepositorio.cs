using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {

        private readonly ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Producto producto)
        {
            var productoBD = _db.Productos.FirstOrDefault(b=> b.Codigo == producto.Codigo);
            if (productoBD != null)
            {
                if(producto.RutaImagen != null)
                {
                    productoBD.RutaImagen = producto.RutaImagen;
                }
                productoBD.Nombre = producto.Nombre;
                productoBD.CodigoLineaComida = producto.CodigoLineaComida;
                productoBD.Contenido = producto.Contenido;

                _db.SaveChanges();
            }
        }
    }
}
