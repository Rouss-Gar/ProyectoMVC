using System.Collections.Generic;

namespace DonChamol.Models.Repository
{
    public interface IMesasRepository<T>
    {
        bool InsertNewMesa(T mesa);           // Método para insertar una mesa
        List<T> GetAllMesas();                // Método para obtener todas las mesas
        List<T> GetByMesaName(string name);   // Método para obtener mesas por nombre
        bool EditMesaById(T mesa);            // Método para editar una mesa por ID
        T GetMesaById(int id);                 // Método para obtener una mesa por ID
    }
}
