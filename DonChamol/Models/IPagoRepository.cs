namespace DonChamol.Models
{
    public interface IPagoRepository<T>
    {
        List<T> GetAllPago();
        T GetPagoById(int id);
        bool InsertNewPago(T Pago);
        bool UpdatePago(T Pago);
        bool DeletePagoById(int id);
    }
}
