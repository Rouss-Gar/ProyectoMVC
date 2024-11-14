namespace DonChamol.Models.Repository
{
    public interface IMesasRepository<T>
    {
        bool InsertNewMesa(T mesa);           // Método para insertar una mesa
        List<T> GetAllMesas();                // Método para obtener todas las mesas    
        bool UpdateMesa(T mesa);                 // Metodo para actualizar mesa
        bool DeleteMesa(int id);
        T GetMesaById(int id);                 // Método para obtener una mesa por ID
    }
}
