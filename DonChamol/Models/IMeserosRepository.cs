namespace DonChamol.Models
{
    public interface IMeserosRepository<T>
    {
        List<T> GetAllMeseros();
        T GetMeseroById(int id);
        bool InsertNewMesero(T meseros);
        bool UpdateMesero(T meseros);
        bool DeleteMeseroById(int id);
        bool ToggleEstado(int id); // Método adicional para cambiar el estado (activo/inactivo) del mesero
        string? GetMeseroByName(string nombre);
    }
}
