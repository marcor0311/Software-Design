using EFoodCommerce.AccesoDatos.Data;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using EFoodCommerce.Utilidades;
using Microsoft.AspNetCore.Http;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public class CarroCompraRepositorio : ICarroCompraRepositorio
    {

        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public CarroCompraRepositorio(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext!.Session;
        }

        public void Agregar(CarroCompra entidad)
        {
            int nuevoCodigo = 1 + UltimoTamano();
            entidad.Codigo = nuevoCodigo;
            List<CarroCompra> listaCarroCompra = (ManejoSession.Get<IEnumerable<CarroCompra>>(_session, "CarroCompra")?? []).ToList();
            listaCarroCompra.Add(entidad);
            ManejoSession.Set(_session, "CarroCompra", listaCarroCompra);
        }

        public void Actualizar(CarroCompra entidad)
        {
            List<CarroCompra> listaCarroCompra = (ManejoSession.Get<IEnumerable<CarroCompra>>(_session, "CarroCompra") ?? []).ToList();
            CarroCompra? carroCompra = listaCarroCompra.FirstOrDefault(c => c.Codigo == entidad.Codigo);
            if (carroCompra != null)
            {
                for (int i = 0; i < listaCarroCompra.Count; i++)
                {
                    if (listaCarroCompra[i].Codigo == entidad.Codigo)
                    {
                        listaCarroCompra[i] = entidad;
                        break;
                    }
                }
                ManejoSession.Set(_session, "CarroCompra", listaCarroCompra);
            }
            else
            {
                Agregar(entidad);
            }
        }
        public bool Contiene(int id)
        {
            List<CarroCompra> listaCarroCompra = (ManejoSession.Get<IEnumerable<CarroCompra>>(_session, "CarroCompra")?? []).ToList();
            return listaCarroCompra.Any(c => c.Codigo == id);
        
        }

        public CarroCompra? Obtener(int id)
        {
            List<CarroCompra> listaCarroCompra = (ManejoSession.Get<IEnumerable<CarroCompra>>(_session, "CarroCompra")?? []).ToList();
            return listaCarroCompra.FirstOrDefault(c => c.Codigo == id);
        }

        public CarroCompra? Remover(int id)
        {
            List<CarroCompra> listaCarroCompra = (ManejoSession.Get<IEnumerable<CarroCompra>>(_session, "CarroCompra")?? []).ToList();
            CarroCompra? carroCompra = listaCarroCompra.FirstOrDefault(c => c.Codigo == id);
            if (carroCompra != null)
            {
                listaCarroCompra.Remove(carroCompra);
                ManejoSession.Set(_session, "CarroCompra", listaCarroCompra);
            }
            return carroCompra;
        }

        public IEnumerable<CarroCompra> ObtenerTodos()
        {
            return ManejoSession.Get<IEnumerable<CarroCompra>>(_session, "CarroCompra")?? [];
        }

        private int UltimoTamano()
        {
            List<CarroCompra> listaCarroCompra = (ManejoSession.Get<IEnumerable<CarroCompra>>(_session, "CarroCompra")?? []).ToList();
            if (listaCarroCompra.Count == 0)
            {
                return 0;
            }
            return listaCarroCompra.Last().Codigo;
        }
    }
}