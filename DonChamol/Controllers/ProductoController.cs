using DonChamol.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;

namespace DonChamol.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProducto<Producto> _repository; 
        private readonly ICategoriaRepository<Categoria> _categoriaRepository; 
        private readonly IProveedor<Proveedor> _proveedorRepository;
        

        public ProductoController (IProducto<Producto> producto, ICategoriaRepository<Categoria> categoriaRepository, IProveedor<Proveedor> proveedor)
        {
            _repository = producto;
            _categoriaRepository = categoriaRepository;
            _proveedorRepository = proveedor;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var productos = _repository.GetAll();
            return View(productos);
        }


        [HttpGet]
        public IActionResult GetById(int id)
        {
            var producto = _repository.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_categoriaRepository.GetAllCategoria(), "id_Categoria", "Nombre");
            ViewBag.Proveedores = new SelectList(_proveedorRepository.GetAll(), "id_proveedor", "nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                var result = _repository.Insert(producto);
                if (result)
                {
                    return RedirectToAction("GetAll");
                }
            }
            ViewBag.Categorias = new SelectList(_categoriaRepository.GetAllCategoria(), "id_Categoria", "Nombre", producto.id_Categoria);
            ViewBag.Proveedores = new SelectList(_proveedorRepository.GetAll(), "id_proveedor", "nombre", producto.id_proveedor);
            return View(producto);
        }

        [HttpGet]
        public IActionResult Edit(int idProducto)
        {
            var producto = _repository.GetById(idProducto);
            if (producto == null)
            {
                return NotFound();
            }
            ViewBag.Categorias = new SelectList(_categoriaRepository.GetAllCategoria(), "id_Categoria", "Nombre");
            ViewBag.Proveedores = new SelectList(_proveedorRepository.GetAll(), "id_proveedor", "nombre");
            return View(producto);
        }

        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                var updated = _repository.Update(producto);
                if (updated)
                {
                    return RedirectToAction("GetAll");
                }
            }
            ViewBag.Categorias = new SelectList(_categoriaRepository.GetAllCategoria(), "id_Categoria", "Nombre", producto.id_Categoria);
            ViewBag.Proveedores = new SelectList(_proveedorRepository.GetAll(), "id_proveedor", "nombre", producto.id_proveedor);
            return View(producto);
        }

        [HttpGet]
        public IActionResult Delete(int id_producto)
        {
            var producto = _repository.GetById(id_producto);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id_producto)
        {
            var producto = _repository.GetById(id_producto);
            if (producto == null)
            {
                return NotFound();
            }
            var result = _repository.Delete(producto);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            return View(producto);
        }

        // Método para alternar el estado de un producto
        [HttpGet]
        public IActionResult ToggleStatus(int id)
        {
            var producto = _repository.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Cambiar el estado
            producto.estado = !producto.estado;

            var updated = _repository.Update(producto);
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
