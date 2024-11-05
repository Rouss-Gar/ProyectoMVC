namespace DonChamol.Models
{
    public class Meseros
    {
        public int id_Mesero { get; set; }
        public object Id_mesero { get; internal set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
    }
}
