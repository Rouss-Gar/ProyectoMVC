using System.ComponentModel.DataAnnotations;

namespace DonChamol.Models
{
    public class Proveedor
    {
        public int id_proveedor { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string? nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido.")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        public string? apellido { get; set; }

        [StringLength(250, ErrorMessage = "La dirección no puede exceder los 250 caracteres.")]
        public string? direccion { get; set; }

        [StringLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "El teléfono solo debe contener números y puede incluir un prefijo de país.")]
        public string? telefono { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string? correo { get; set; }

        public bool estado { get; set; }

        // Relación uno a muchos con Producto
        public ICollection<Producto>? Productos { get; set; }
    } 
}

