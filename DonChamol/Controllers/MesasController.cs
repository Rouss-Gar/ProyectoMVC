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

        [HttpGet]
        public IActionResult GetAllMesas()
        {
            List<Mesas> mesas = _repository.GetAllMesas();
            return View(mesas);
        }

        [HttpGet]
        public IActionResult GetByMesasName(string numeroMesa)
        {
            var mesas = _repository.GetByMesaName(numeroMesa);
            return View("GetByMesasName", mesas);
        }

        

        [HttpGet]
        public IActionResult EditMesaById(int id)
        {
            var mesas = _repository.GetMesaById(id);
            if (mesas == null)
            {
                return NotFound();
            }
            return View("GetByMesasName", mesas);
        }

        [HttpPost]
        public IActionResult EditMesaById(Mesas mesas)
        {
            if (ModelState.IsValid)
            {
                bool result = _repository.EditMesaById(mesas);
                if (result)
                {
                    return RedirectToAction("GetAllMesas");
                }
                ModelState.AddModelError("", "Error al actualizar la mesa.");
            }
            return View(mesas);
        }


    }
}
