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

        public MenuItemsController(IMenuItemsRepository<MenuItems> menuItemsRepository,
                                   ICategoriaRepository<Categoria> categoriaRepository)
        {
            _menuItemsRepository = menuItemsRepository ?? throw new ArgumentNullException(nameof(menuItemsRepository));
            _categoriaRepository = categoriaRepository ?? throw new ArgumentNullException(nameof(categoriaRepository));
        }

        public IActionResult GetAllMenuItems()
        {
            var menuItems = _menuItemsRepository.GetAllMenuItems();

            // Asegúrate de que cada menú item tenga su categoría cargada
            foreach (var item in menuItems)
            {
                item.Categoria = _categoriaRepository.GetCategoriaById(item.id_Categoria);
            }

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

        [HttpGet]
        public IActionResult EditMenuItems(int id)
        {
            var menuItem = _menuItemsRepository.GetMenuItemById(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            // Asegúrate de que las categorías están cargadas en el ViewBag
            ViewBag.Categorias = _categoriaRepository.GetAllCategoria();
            return View(menuItem);
        }

        [HttpPost]
        public IActionResult EditMenuItems(MenuItems menuItems)
        {
            if (ModelState.IsValid)
            {
                bool isEdited = _menuItemsRepository.EditMenuItem(menuItems);
                if (isEdited)
                {
                    return RedirectToAction("GetAllMenuItems");
                }
            }

            // Recargar categorías en caso de fallo de validación
            ViewBag.Categorias = _categoriaRepository.GetAllCategoria();
            return View(menuItems);
        }

        [HttpPost]
        public IActionResult DeleteMenuItemById(int id)
        {
            bool isDeleted = _menuItemsRepository.DeleteMenuItemById(id);
            if (isDeleted)
            {
                return RedirectToAction("GetAllMenuItems");
            }
            return RedirectToAction("GetAllMenuItems");
        }

        [HttpPost]
        public IActionResult ToggleEstado(int id)
        {
            bool isToggled = _menuItemsRepository.ToggleEstado(id);
            return RedirectToAction("GetAllMenuItems");
        }

    }
}
