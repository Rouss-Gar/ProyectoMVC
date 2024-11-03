namespace DonChamol.Models
{
    public interface IClienteRepository<T>
    {
        List<T> GetAllCliente();
        T GetClienteById(int id);
        bool InsertNewCliente(T cliente);
        bool UpdateCliente(T cliente);
        bool DeleteClienteById(int id);
        T GetClienteByName(string nombre); // Cambia el tipo a T o Cliente según lo que necesites
    }
}

