using Microsoft.AspNetCore.Mvc;
using DonChamol.Models;
using DonChamol.Models.Repository;
using System.Collections.Generic;

namespace DonChamol.Controllers
{
    public class OrdenController : Controller
    {
        private readonly IOrdenRepository<GetAllOrden> _repository;

        public OrdenController(IOrdenRepository<GetAllOrden> repository)
        {
            _repository = repository;
        }

        // GET: /Orden/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crear nueva orden
        [HttpPost]
        public IActionResult Create(GetAllOrden orden)
        {
            if (ModelState.IsValid)
            {
                // Inserta la nueva orden en el repositorio
                _repository.InsertNewOrden(orden);
                // Redirige a la lista de órdenes después de la creación
                return RedirectToAction("GetAllOrdenes");
            }

            // Si el modelo no es válido, regresa a la vista Create
            return View(orden);
        }

        // GET: /Orden/GetAllOrdenes
        [HttpGet]
        public IActionResult GetAllOrdenes()
        {
            List<GetAllOrden> ordenes = _repository.GetAllOrden();
            return View(ordenes);
        }
    }
}
