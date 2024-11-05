using System.Collections.Generic;

namespace DonChamol.Models
{
    public interface IMenuItemsRepository<T>
    {
        List<T> GetAllMenuItems();
        T GetMenuItemById(int id);
        bool InsertNewMenuItem(T menuItems);
        bool UpdateMenuItem(T menuItems);
        bool DeleteMenuItemById(int id);
    }
}
