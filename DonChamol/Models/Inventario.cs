namespace DonChamol.Models
{
    public class Inventario
    {
        public int InventarioID { get; set; }

        public int id_producto { get; set; }

        public string nombre_producto { get; set; }

        public int UnidadesEnStock { get; set; }

        public decimal PrecioUnitario { get; set; }
    }
}
