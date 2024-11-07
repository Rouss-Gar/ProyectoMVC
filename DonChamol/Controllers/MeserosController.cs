using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DonChamol.Models;
using System.Diagnostics.Metrics;

namespace DonChamol.Controllers
{
    public class MeserosController : Controller
    {
        private readonly IMeserosRepository<Meseros> _repository;

        public MeserosController(IMeserosRepository<Meseros> repository)
        {
            _repository = repository;
        }

        // GET: List all waiters
        [HttpGet]
        public IActionResult GetAllMeseros()
        {
            try
            {
                List<Meseros> meseros = _repository.GetAllMeseros();

                if (meseros == null || meseros.Count == 0)
                {
                    ModelState.AddModelError("", "No se encontraron meseros en la base de datos.");
                }

                return View(meseros);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al cargar la lista de meseros: " + ex.Message);
                return View(new List<Meseros>());
            }
        }

   

        // GET: Show create waiter form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create new waiter
        [HttpPost]
        public IActionResult Create(Meseros meseros)
        {
            if (ModelState.IsValid)
            {
                _repository.InsertNewMesero(meseros);
                return RedirectToAction("GetAllMeseros");
            }
            return View(meseros);
        }
        [HttpGet]
        public IActionResult GetMeseroById(int id)
        {
            var meseros = _repository.GetMeseroById(id);
            if (meseros == null)
            {
                return NotFound();
            }
            return View(meseros);
        }

        [HttpGet]
        public IActionResult UpdateMesero(int id)
        {
            var meseros = _repository.GetMeseroById(id);
            if (meseros == null)
            {
                return NotFound();
            }
            return View(meseros);
        }



        [HttpPost]
        public IActionResult UpdateMesero(Meseros meseros)
        {
            if (ModelState.IsValid)
            {
                var result = _repository.UpdateMesero(meseros);
                if (result)
                {
                    return RedirectToAction("GetAllMeseros");
                }
                ModelState.AddModelError("", "No se pudo actualizar el mesero.");
            }
            return View(meseros);
        }




        [HttpPost]
        public IActionResult DeleteMeseroById(int id)
        {
            bool isDeleted = _repository.DeleteMeseroById(id);
            if (isDeleted)
            {
                return RedirectToAction("GetAllMeseros");
            }
            return RedirectToAction("GetAllMeseros"); // O maneja el error de manera adecuada
        }

        // POST: Toggle waiter status (Active/Inactive)
        [HttpPost]
        public IActionResult ToggleEstado(int id)
        {
            var meseros = _repository.GetMeseroById(id);
            if (meseros != null)
            {
                meseros.Estado = !meseros.Estado;
                _repository.UpdateMesero(meseros);
            }
            return RedirectToAction("GetAllMeseros");
        }
    }
}
