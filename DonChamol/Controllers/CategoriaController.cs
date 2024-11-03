using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DonChamol.Models;

namespace DonChamol.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository<Categoria> _repository;

        public CategoriaController(ICategoriaRepository<Categoria> repository)
        {
            _repository = repository;
        }

        // GET: List all categories
        [HttpGet]
        public IActionResult GetAllCategoria()
        {
            List<Categoria> categorias = _repository.GetAllCategoria();
            return View(categorias);
        }

        // GET: Get category by ID
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

        // GET: Get category by name
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var categoria = _repository.GetCategoriaById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }



        // GET: Show create category form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create new category
        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _repository.InsertNewCategoria(categoria);
                return RedirectToAction("Index"); 
            }
            return View(categoria);
        }


        // POST: Edit category by name
        [HttpPost]
        public IActionResult EditCategory(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = _repository.EditCategoriaByName(categoria);
                if (isUpdated)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return View(categoria);
        }

        // GET: Show edit form by category ID
        [HttpGet]
        public IActionResult EditCategoryById(int id)
        {
            var categoria = _repository.GetCategoriaById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Edit category by ID
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


        // POST: Delete category by ID
        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _repository.DeleteCategoriaById(id);
            if (isDeleted)
            {
                return RedirectToAction("GetAll");
            }
            return RedirectToAction("GetAll"); // Or handle the error appropriately
        }

        // POST: Toggle category status (Active/Inactive)
        [HttpPost]
        public IActionResult ToggleEstado(int id)
        {
            var categoria = _repository.GetCategoriaById(id);
            if (categoria != null)
            {
                categoria.Estado = !categoria.Estado;
                _repository.UpdateCategoria(categoria);
            }
            return RedirectToAction("GetAll");
        }

        // Método POST para procesar la actualización de la categoría
        [HttpPost]
        public IActionResult EditCategoryById(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var result = _repository.UpdateCategoria(categoria);
                if (result)
                {
                    return RedirectToAction("Index"); // Redirige al listado de categorías después de actualizar
                }
                ModelState.AddModelError("", "No se pudo actualizar la categoría.");
            }
            return View(categoria);
        }

        // POST: Delete category by ID
        [HttpPost]
        public IActionResult DeleteCategoriaById(int id)
        {
            bool isDeleted = _repository.DeleteCategoriaById(id);
            if (isDeleted)
            {
                return RedirectToAction("GetAllCategoria");
            }
            else
            {
                // Si la eliminación falla, puedes agregar un mensaje de error o manejar la redirección
                ModelState.AddModelError("", "No se pudo eliminar la categoría.");
                return RedirectToAction("GetAllCategoria");
            }
        }


    }
}
