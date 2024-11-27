using DonChamol.Models.Dto;

namespace DonChamol.Models
{
    public class CrearVentaViewModel
    {
        public List<ClienteDto> Clientes { get; set; }
        public List<ProductoDto> Productos { get; set; }

        public List<MeseroDto> Meseros { get; set; }
    }
}
