namespace DonChamol.Models
{
    public class OrdenDetalle
    {
        public int id_OrdenDetalle { get; set; }
        public int id_Orden { get; set; }
        public int id_Menu { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
    }
}
