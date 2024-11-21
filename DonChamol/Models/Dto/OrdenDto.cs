namespace DonChamol.Models.Dto
{
    public class OrdenDto
    {
        public int id_Orden { get; set; }
        public int id_Cliente { get; set; }
        public int id_Mesero { get; set; }
        public int id_Mesa { get; set; }
        public int Total { get; set; }
        public DateTime Fecha_Orden
        {
            get; set;
        }
    }
}
