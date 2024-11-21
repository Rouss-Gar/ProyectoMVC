namespace DonChamol.Models.Repository
{
    public interface IGetAllOrdenRepository<T>
    {
        List<T> GetAllOrden();              // Obtener todas las órdenes
        T GetOrdenById(int id);             // Obtener una orden por su ID
        bool InsertOrden(T orden);          // Insertar una nueva orden
        bool UpdateOrden(T orden);          // Actualizar una orden existente
        bool DeleteOrdenById(int id);       // Eliminar una orden por su ID
    }
}