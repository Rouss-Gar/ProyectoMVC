namespace DonChamol.Models
{
    public interface ICategoriaRepository<T>
    {
        List<T> GetAllCategoria();              
        T GetCategoriaById(int id);               
        bool InsertNewCategoria(T categoria);     
        bool UpdateCategoria(T categoria);        
        bool EditCategoriaByName(T category);    
        bool DeleteCategoriaById(int id);        
    }
}