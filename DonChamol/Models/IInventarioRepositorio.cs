namespace DonChamol.Models
{
    public interface IInventarioRepositorio<T>
    {
        List<T> GetAll();

        decimal? ObtenerPrecioProducto(int idProducto);
    }
}
