namespace DonChamol.Models.Dto
{
    public class VentaDto
    {
        public int id_Cliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public List<DetalleVentaDto> DetallesVenta { get; set; }

        public int id_Mesero { get; set; }
    }
}
