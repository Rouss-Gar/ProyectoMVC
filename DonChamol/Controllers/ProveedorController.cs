using DonChamol.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace DonChamol.Controllers
{
    public class ProveedorController : Controller
    {

        private readonly IProveedor<Proveedor> _repository;

        public ProveedorController(IProveedor<Proveedor> repository)
        {
            _repository = repository;
        }
    
        [HttpGet]
        public IActionResult GetAll()
        {
            var Proveedor = _repository.GetAll();
            return View(Proveedor);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var proveedor = _repository.GetById(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                var result = _repository.Insert(proveedor);
                if (result)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return View(proveedor);
        }

        [HttpGet]
        public IActionResult EditProveedor(int id)
        {
            var proveedor = _repository.GetById(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        [HttpPost]
        public IActionResult EditProveedor(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                var updated = _repository.Update(proveedor);
                if (updated)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return View(proveedor);
        }

        [HttpGet]
        public IActionResult Delete(int id_proveedor)
        {
            var proveedor = _repository.GetById(id_proveedor);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id_proveedor)
        {
            var proveedor = _repository.GetById(id_proveedor);
            if (proveedor == null)
            {
                return NotFound();
            }

            var result = _repository.Delete(proveedor);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            return View(proveedor);
        }

        // Método para alternar el estado de un producto
        [HttpGet]
        public IActionResult ToggleStatus(int id)
        {
            var proveedor = _repository.GetById(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            // Cambiar el estado
            proveedor.estado = !proveedor.estado;

            var updated = _repository.Update(proveedor);
            if (updated)
            {
                return RedirectToAction("GetAll");
            }

            // Maneja el error si la actualización falla
            TempData["Error"] = "No se pudo cambiar el estado del producto.";
            return RedirectToAction("GetAll");
        }
    }
}
