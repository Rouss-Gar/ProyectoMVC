namespace DonChamol.Models
{
    public class RegistroDetalleVenta
    {
        public int DetalleVentaID { get; set; }


        public int id_producto { get; set; }


        public string nombre_producto { get; set; }


        public int Cantidad { get; set; }


        public decimal PrecioUnitario { get; set; }

        public decimal TotalLinea { get; set; }
    }
}
