namespace DonChamol.Models
{
    public class RegistroVenta
    {
        public int VentaID { get; set; }


        public int id_Cliente { get; set; }

        public int id_Mesero { get; set; }
        public string Nombre { get; set; }


        public DateTime FechaVenta { get; set; }


        public decimal MontoTotal { get; set; }


        public List<RegistroDetalleVenta> DetallesVenta { get; set; }

        public RegistroVenta()
        {
            DetallesVenta = new List<RegistroDetalleVenta>();
        }
    }
}
