namespace DonChamol.Models
{
    public interface IOrdenRepository<T>
    {
        List<T> GetAllOrden();                // Obtener todas las órdenes
        T GetOrdenById(int id);               // Obtener una orden por su ID
        bool InsertNewOrden(T orden);         // Insertar una nueva orden
        bool UpdateOrden(T orden);            // Actualizar una orden existente
        bool DeleteOrdenById(int id);         // Eliminar una orden por su ID
    }
}
