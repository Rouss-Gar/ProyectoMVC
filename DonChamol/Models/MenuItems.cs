namespace DonChamol.Models
{
    public class MenuItems
    {
        public int id_Menu { get; set; }

        public required string Nombre { get; set; }

        public required string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public bool Estado { get; set; } = true;

        public int id_Categoria { get; set; }

        public Categoria? Categoria { get; set; }
    }
}