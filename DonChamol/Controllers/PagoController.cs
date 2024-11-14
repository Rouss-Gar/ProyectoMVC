using DonChamol.Models;
using Microsoft.AspNetCore.Mvc;

namespace DonChamol.Controllers
{
    public class PagoController : Controller
    {
        private readonly IPagoRepository<Pago> _repository;

        public PagoController(IPagoRepository<Pago> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllPago()
        {
            List<Pago> Pago = _repository.GetAllPago();
            return View(Pago);
        }


        // GET: Pagos/Details/5
        public IActionResult GetPagoById(int id)
        {
            var Pago = _repository.GetPagoById(id);
            if (Pago == null)
            {
                return NotFound();
            }
            return View(Pago);
        }

        // GET: Pagos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pagos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pago Pago)
        {
            if (ModelState.IsValid)
            {
                var pago = _repository.InsertNewPago(Pago);
                if (pago)
                {
                    return RedirectToAction("GetAllPago");
                }
                ModelState.AddModelError("", "No se pudo crear el pago.");
            }
            return View(Pago);
        }

        [HttpGet]
        public IActionResult EditPagoById(int id)
        {
            var Pago = _repository.GetPagoById(id);
            if (Pago == null)
            {
                return NotFound();
            }
            return View(Pago);
        }

        [HttpPost]
        public IActionResult EditPagoById(Pago Pago)
        {
            if (ModelState.IsValid)
            {
                var pago = _repository.UpdatePago(Pago);
                if (pago)
                {
                    return RedirectToAction("GetAllPago");
                }
                ModelState.AddModelError("", "No se pudo actualizar el Cliente.");
            }
            return View(Pago);
        }


    }
}