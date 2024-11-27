namespace DonChamol.Models
{
    public interface IOrdenRepository<T>
    {
        List<T> GetAllOrden();                // Obtener todas las órdenes
        T GetOrdenById(int id);               // Obtener una orden por su ID
        bool InsertNewOrden(T orden, List<DetalleOrden> detalles); // Insertar una nueva orden con detalles
        bool DeleteOrdenById(int id);         // Eliminar una orden por su ID
    }
}
