namespace DonChamol.Models
{
    public class Cliente
    {
        public int id_Cliente { get; set; } 
        public string Nombre{ get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaRegistro { get; set; } 
        public bool Estado { get; set; }
    }
}
