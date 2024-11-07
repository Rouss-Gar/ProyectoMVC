using System.Collections.Generic;

namespace DonChamol.Models
{
    public interface IMenuItemsRepository<T>
    {
        List<T> GetAllMenuItems();

        T? GetMenuItemById(int id);

        bool InsertNewMenuItem(T menuItems);

        // Método para editar un elemento de menú
        bool EditMenuItem(T menuItems);

        // Método para eliminar un elemento de menú por su ID
        bool DeleteMenuItemById(int id);

        // Método para cambiar el estado de un elemento de menú
        bool ToggleEstado(int id);
    }
}
