using DonChamol.Models.Dto;

namespace DonChamol.Models
{
    public interface IVentasRepositorio
    {
        List<ProductoDto> ObtenerProductos();
        List<ClienteDto> ObtenerClientes();

        bool InsertarVenta(Venta venta, List<DetalleVenta> detallesVenta);
        IEnumerable<RegistroVenta> ObtenerVentas();
    }
}
