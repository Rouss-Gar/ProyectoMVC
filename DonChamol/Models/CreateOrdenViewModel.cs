using DonChamol.Models.Dto;

namespace DonChamol.Models
{
    public class CreateOrdenViewModel
    {
        public List<ClienteDto> Clientes { get; set; }
        public List<MeseroDto> Meseros { get; set; }
        public List<MesaDto> Mesas { get; set; }
        public List<MenuDto> MenuItems { get; set; }
    }
}
