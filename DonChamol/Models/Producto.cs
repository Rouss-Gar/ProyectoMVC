using System.ComponentModel.DataAnnotations;

namespace DonChamol.Models
{
    public class Producto
    {
        public int id_producto { get; set; }

        [Required(ErrorMessage = "La categoría es requerida.")]
        public int id_Categoria { get; set; }

        [Required(ErrorMessage = "El proveedor es requerido.")]
        public int id_proveedor { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres.")]
        public string? nombre_producto { get; set; }



        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string? descripcion { get; set; }


        [Required(ErrorMessage = "la fecha es requerida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? fecha_vencimiento { get; set; }

        public bool estado { get; set; }

        // Propiedades de navegación (Muchos-a-Uno)
        public Categoria? Categoria { get; set; }
        public Proveedor? Proveedor { get; set; }
    }
}
