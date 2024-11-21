namespace DonChamol.Models.Dto
{
    public class DetalleOrdenConInfoDto
    {
        public int id_OrdenDetalle { get; set; }
        public int id_MenuItem { get; set; }
        public string NombreMenuItem { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalLinea { get; set; }
    }
}
