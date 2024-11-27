using DonChamol.Models.Dto;
using DonChamol.Models;
using Microsoft.AspNetCore.Mvc;

namespace DonChamol.Controllers
{
    public class VentasController : Controller
    {
        private readonly IVentasRepositorio _repository;
        private readonly IMeserosRepository<Meseros> _entityCrud;
        private readonly IInventarioRepositorio<Inventario> _inventarioRepositorio;


        public VentasController(IVentasRepositorio repository, IMeserosRepository<Meseros> crud, IInventarioRepositorio<Inventario> inventarioRepositorio)
        {
            _repository = repository;
            _entityCrud = crud;
            _inventarioRepositorio = inventarioRepositorio;

        }

        [HttpGet]
        public IActionResult Get()
        {
            var ventas = _repository.ObtenerVentas();
            return View(ventas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var clientes = _repository.ObtenerClientes();
            var productos = _repository.ObtenerProductos();
            var meseros = _entityCrud.GetAllMeseros();

            var model = new CrearVentaViewModel
            {
                Clientes = clientes.Select(c => new ClienteDto { id_Cliente = c.id_Cliente, Nombre = c.Nombre }).ToList(),
                Productos = productos.Select(p => new ProductoDto { id_producto = p.id_producto, nombre_producto = p.nombre_producto }).ToList(),
                Meseros = meseros.Select(e => new MeseroDto { id_Mesero = e.id_Mesero, Nombre = e.Nombre }).ToList()
            };

            return View("Create", model);
        }

        [HttpPost]
        public IActionResult CrearVenta([FromBody] VentaDto ventaDto)
        {
            var venta = new Venta
            {
                id_Cliente = ventaDto.id_Cliente,
                id_Mesero = ventaDto.id_Mesero,
                FechaVenta = ventaDto.FechaVenta
            };

            var detallesVenta = ventaDto.DetallesVenta.Select(d => new DetalleVenta
            {
                id_producto = d.id_producto,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            }).ToList();

            var resultado = _repository.InsertarVenta(venta, detallesVenta);

            if (resultado)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        public IActionResult ObtenerPrecioProducto(int idProducto)
        {
            var precio = _inventarioRepositorio.ObtenerPrecioProducto(idProducto);
            return precio.HasValue
                ? Json(new { success = true, precio })
                : Json(new { success = false, mensaje = "Producto no encontrado." });
        }

    }
}
