namespace DonChamol.Models
{
    public class Pago
    {
        public int id_Pago { get; set; }
        public string id_Orden { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public DateTime Fecha_Pago { get; set; }
    }
}

