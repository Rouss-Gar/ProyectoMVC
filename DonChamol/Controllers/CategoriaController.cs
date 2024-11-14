using DonChamol.Models;
using Microsoft.AspNetCore.Mvc;

namespace DonChamol.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository<Categoria> _repository;

        public CategoriaController(ICategoriaRepository<Categoria> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllCategoria()
        {
            List<Categoria> categorias = _repository.GetAllCategoria();
            return View(categorias);
        }



        [HttpGet]
        public IActionResult GetCategoriaById(int id)
        {
            var categoria = _repository.GetCategoriaById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _repository.InsertNewCategoria(categoria);
                return RedirectToAction("GetAllCategoria");
            }
            return View(categoria);
        }

        [HttpPost]
        public IActionResult ToggleEstado(int id)
        {
            var categoria = _repository.GetCategoriaById(id);
            if (categoria != null)
            {
                categoria.Estado = !categoria.Estado;
                _repository.UpdateCategoria(categoria);
            }
            return RedirectToAction("GetAllCategoria");
        }

        [HttpGet]
        public IActionResult EditCategoriaById(int id)
        {
            var categoria = _repository.GetCategoriaById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        public IActionResult EditCategoriaById(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var result = _repository.UpdateCategoria(categoria);
                if (result)
                {
                    return RedirectToAction("GetAllCategoria");
                }
                ModelState.AddModelError("", "No se pudo actualizar la categoría.");
            }
            return View(categoria);
        }
    }
}
/*Comentario Random*/