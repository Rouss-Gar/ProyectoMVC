namespace DonChamol.Models
{
    public interface IProveedor<T>
    {
        List<T> GetAll();

        T GetById(int id);

        bool Insert(T model);

        bool Update(T model);

        bool Delete(T model);
    }
}
