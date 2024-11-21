namespace DonChamol.Models
{
    public class OrdenConDetalles
    {
        public int id_Orden { get; set; }
        public int id_Cliente { get; set; }
        public string NombreCliente { get; set; }
        public int id_Mesero { get; set; }
        public string NombreMesero { get; set; }
        public int id_Mesa { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
    }
}

