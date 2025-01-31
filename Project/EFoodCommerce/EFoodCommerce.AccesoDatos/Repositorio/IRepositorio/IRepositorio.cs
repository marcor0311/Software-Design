using System.Linq.Expressions;
using EFoodCommerce.Modelos;
using EFoodCommerce.Modelos.Especificaciones;

namespace EFoodCommerce.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class, IEntity
    {

        Task<T?> Obtener(int id);

        bool Contiene(int id);

        void Editar(T entidad);

        Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? incluirPropiedades = null, bool isTracking = true);

        PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>>? filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? incluirPropiedades = null, bool isTracking = true);


        Task<T?> ObtenerPrimero(Expression<Func<T, bool>>? filtro = null,
            string? incluirPropiedades = null, bool isTracking = true);

        Task Agregar(T entidad);

        void Remover(T entidad);

    }
}