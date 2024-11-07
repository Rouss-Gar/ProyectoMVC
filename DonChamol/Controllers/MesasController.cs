using Microsoft.AspNetCore.Mvc;
using DonChamol.Models;
using DonChamol.Models.Repository;
using System.Collections.Generic;

namespace DonChamol.Controllers
{
    public class MesasController : Controller
    {
        private readonly IMesasRepository<Mesas> _repository;

        public MesasController(IMesasRepository<Mesas> repository)
        {
            _repository = repository;
        }

        // GET: /Mesas/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create new mesa
        [HttpPost]
        public IActionResult Create(Mesas mesas)
        {
            if (ModelState.IsValid)
            {
                // Inserta la nueva mesa en el repositorio
                _repository.InsertNewMesa(mesas);
                // Redirige a la lista de mesas después de la creación
                return RedirectToAction("GetAllMesas");
            }

            // Si el modelo no es válido, regresa a la vista Create
            return View(mesas);
        }


        [HttpGet]
        public IActionResult GetAllMesas()
        {
            List<Mesas> mesas = _repository.GetAllMesas();
            return View(mesas);
        }


        [HttpPost]
        public IActionResult ToggleEstado(int id)
        {
            var mesas = _repository.GetMesaById(id);
            if (mesas != null)
            {
                mesas.Estado = !mesas.Estado;
                _repository.UpdateMesa(mesas);
            }
            return RedirectToAction("GetAllMesas");
        }


        [HttpPost]
        public IActionResult DeleteMesa(int id)
        {
            bool isDeleted = _repository.DeleteMesa(id);
            if (isDeleted)
            {
                return RedirectToAction("GetAllMesas");
            }
            return RedirectToAction("GetAllMesas"); // Or handle the error appropriately
        }
    }






    
}
