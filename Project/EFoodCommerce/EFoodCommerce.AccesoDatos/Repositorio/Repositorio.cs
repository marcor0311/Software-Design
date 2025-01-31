using EFoodCommerce.AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;
using EFoodCommerce.AccesoDatos.Repositorio.IRepositorio;
using EFoodCommerce.Modelos;
using System.Linq.Expressions;
using EFoodCommerce.Modelos.Especificaciones;

namespace EFoodCommerce.AccesoDatos.Repositorio
{
    public abstract class Repositorio<T> : IRepositorio<T> where T : class, IEntity
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }


        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad);
        }

        public bool Contiene(int id)
        {
            return Obtener(id) != null;
        }

        public void Editar(T entidad)
        {
            dbSet.Update(entidad);
        }

        public async Task<T?> Obtener(int id)
        {
            return await dbSet.FindAsync(id);
        }


        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if(filtro !=null)
            {
                query = query.Where(filtro);
            }
            if(incluirPropiedades !=null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }
            if(orderBy !=null)
            {
                query = orderBy(query);
            }
            if(!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();

        }

        public PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>>? filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return PagedList<T>.ToPagedList(query, parametros.PageNumber, parametros.PageSize);
        }

        public async Task<T?> ObtenerPrimero(Expression<Func<T, bool>>? filtro = null,
            string? incluirPropiedades = null, bool isTracking = true)
        {

            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); 
                }
            }
          
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }

    }



}