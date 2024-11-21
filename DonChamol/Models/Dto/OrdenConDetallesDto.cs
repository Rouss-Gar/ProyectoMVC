namespace DonChamol.Models.Dto
{
    public class OrdenConDetallesDto
    {
        public int id_Orden { get; set; }
        public int id_Cliente { get; set; }
        public string ClienteNombre { get; set; }
        public int id_Mesero { get; set; }
        public string MeseroNombre { get; set; }
        public int id_Mesa { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleOrdenConInfoDto> Detalles { get; set; }
    }
}
