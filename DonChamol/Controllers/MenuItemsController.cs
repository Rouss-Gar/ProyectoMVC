using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DonChamol.Models;
using NuGet.Protocol.Core.Types;
using DonChamol.Models.Repository;


namespace DonChamol.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly IMenuItemsRepository<MenuItems> _repository;
        private readonly ICategoriaRepository<Categoria> _categoriaRepository;


        public MenuItemsController(IMenuItemsRepository<MenuItems> repository)
        {
            _repository = repository;
        }



        // GET: List all menu items
        [HttpGet]
        public IActionResult GetAllMenuItems()
        {
            List<MenuItems> menuItems = _repository.GetAllMenuItems();
            return View(menuItems);
        }


        // GET: Get menu item by ID
        [HttpGet]
        public IActionResult GetMenuItemById(int id)
        {
            var menuItems = _repository.GetMenuItemById(id);
            if (menuItems == null)
            {
                return NotFound();
            }
            return View(menuItems);
        }

        // GET: Show create menu item form
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categorias = _categoriaRepository.GetAllCategoria();
            return View();
        }

        // POST: Create new menu item
        [HttpPost]
        public IActionResult Create(MenuItems menuItems)
        {
            if (ModelState.IsValid)
            {
                _repository.InsertNewMenuItem(menuItems);
                return RedirectToAction("GetAllMenuItems");
            }
            return View(menuItems);
        }

        // GET: Show edit form for menu item by ID
        [HttpGet]
        public IActionResult EditMenuItemById(int id)
        {
            var menuItems = _repository.GetMenuItemById(id);
            if (menuItems == null)
            {
                return NotFound();
            }
            ViewBag.Categorias = _categoriaRepository.GetAllCategoria();
            return View(menuItems);
        }

        // POST: Edit menu item by ID
        [HttpPost]
        public IActionResult EditMenuItemById(MenuItems menuItems)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = _repository.UpdateMenuItem(menuItems);
                if (isUpdated)
                {
                    return RedirectToAction("GetAllMenuItems");
                }
                ModelState.AddModelError("", "No se pudo actualizar el item del menú.");
            }
            return View(menuItems);
        }

        // POST: Delete menu item by ID
        [HttpPost]
        public IActionResult DeleteMenuItemById(int id)
        {
            bool isDeleted = _repository.DeleteMenuItemById(id);
            if (isDeleted)
            {
                return RedirectToAction("GetAllMenuItems");
            }
            ModelState.AddModelError("", "No se pudo eliminar el item del menú.");
            return RedirectToAction("GetAllMenuItems");
        }

        // POST: Toggle menu item status (Active/Inactive)
        [HttpPost]
        public IActionResult ToggleEstado(int id)
        {
            var menuItems = _repository.GetMenuItemById(id);
            if (menuItems != null)
            {
                menuItems.Estado = !menuItems.Estado;
                _repository.UpdateMenuItem(menuItems);
            }
            return RedirectToAction("GetAllMenuItems");
        }
    }
}
