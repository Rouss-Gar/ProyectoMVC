namespace DonChamol.Models
{
    public class Orden
    {
        public int id_Orden { get; set; }
        public int id_Cliente { get; set; }
        public int id_Mesero { get; set; }
        public int id_Mesa { get; set; }
        public int id_Menu { get; set; }
        public DateTime Fecha_Orden { get; set; }
        public decimal Total { get; set; }
    }
}
