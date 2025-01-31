using EFoodCommerce.Modelos;

namespace EFoodCommerce.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICarroCompraRepositorio
    {
        public void Agregar(CarroCompra entidad);
        public void Actualizar(CarroCompra entidad);

        public bool Contiene(int id);

        public CarroCompra? Obtener(int id);

        public CarroCompra? Remover(int id);

        public IEnumerable<CarroCompra> ObtenerTodos();

    }
}
