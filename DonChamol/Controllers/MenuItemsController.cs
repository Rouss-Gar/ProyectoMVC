using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DonChamol.Models;
using NuGet.Protocol.Core.Types;
using DonChamol.Models.Repository;


namespace DonChamol.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly IMenuItemsRepository<MenuItems> _menuItemsRepository;
        private readonly ICategoriaRepository<Categoria> _categoriaRepository;

        // Constructor con las dependencias necesarias
        public MenuItemsController(IMenuItemsRepository<MenuItems> menuItemsRepository,
                                   ICategoriaRepository<Categoria> categoriaRepository)
        {
            _menuItemsRepository = menuItemsRepository ?? throw new ArgumentNullException(nameof(menuItemsRepository));
            _categoriaRepository = categoriaRepository ?? throw new ArgumentNullException(nameof(categoriaRepository));
        }

        public IActionResult GetAllMenuItems()
        {
            var menuItems = _menuItemsRepository.GetAllMenuItems();
            return View(menuItems);
        }


        // GET: Get menu item by ID
        [HttpGet]
        public IActionResult GetMenuItemById(int id)
        {
            var menuItems = _menuItemsRepository.GetMenuItemById(id);
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
            // Verifica que la lista de categorías no sea nula ni vacía
            var categorias = _categoriaRepository.GetAllCategoria();
            if (categorias != null && categorias.Any())
            {
                ViewBag.Categorias = categorias;
            }
            else
            {
                ViewBag.Categorias = new List<Categoria>();  // Para evitar nulos
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(MenuItems menuItems)
        {
            if (ModelState.IsValid)
            {
                bool isInserted = _menuItemsRepository.InsertNewMenuItem(menuItems);
                if (isInserted)
                {
                    return RedirectToAction("GetAllMenuItems");
                }
            }

            // Recargar categorías en caso de que el modelo no sea válido
            ViewBag.Categorias = _categoriaRepository.GetAllCategoria();
            return View(menuItems);
        }
        // GET: Show edit form for menu item by ID
        [HttpGet]
        public IActionResult EditMenuItemById(int id)
        {
            var menuItems = _menuItemsRepository.GetMenuItemById(id);
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
                bool isUpdated = _menuItemsRepository.UpdateMenuItem(menuItems);
                if (isUpdated)
                {
                    return RedirectToAction("GetAllMenuItems");
                }
                ModelState.AddModelError("", "No se pudo actualizar el item del menú.");
            }
            // Vuelve a cargar las categorías si se vuelve a la vista por error
            ViewBag.Categorias = _categoriaRepository.GetAllCategoria();
            return View(menuItems);
        }


        // POST: Delete menu item by ID
        [HttpPost]
        public IActionResult DeleteMenuItemById(int id)
        {
            bool isDeleted = _menuItemsRepository.DeleteMenuItemById(id);
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
            var menuItems = _menuItemsRepository.GetMenuItemById(id);
            if (menuItems != null)
            {
                menuItems.Estado = !menuItems.Estado;
                _menuItemsRepository.UpdateMenuItem(menuItems);
            }
            return RedirectToAction("GetAllMenuItems");
        }
    }
}
