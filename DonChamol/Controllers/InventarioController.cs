using DonChamol.Models;
using Microsoft.AspNetCore.Mvc;

namespace DonChamol.Controllers
{
    public class InventarioController : Controller
    {
        private readonly IInventarioRepositorio<Inventario> inventarioRepositorio;

        public InventarioController(IInventarioRepositorio<Inventario> _inventarioRepositorio)
        {
            inventarioRepositorio = _inventarioRepositorio;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var inventario = inventarioRepositorio.GetAll();
            return View(inventario);
        }
    }
}
