namespace DonChamol.Models
{
    public class Venta
    {
        public int VentaID { get; set; }
        public int id_Cliente { get; set; }

        public int id_Mesero { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
