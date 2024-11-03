using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DonChamol.Models;

namespace DonChamol.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository<Cliente> _repository;

        public ClienteController(IClienteRepository<Cliente> repository)
        {
            _repository = repository;
        }


        // GET: List all clients
        [HttpGet]
        public IActionResult GetAllCliente()
        {
            try
            {
                List<Cliente> clientes = _repository.GetAllCliente();

                // Verificar si clientes es nulo o vacío y registrar un mensaje si es necesario
                if (clientes == null || clientes.Count == 0)
                {
                    ModelState.AddModelError("", "No se encontraron clientes en la base de datos.");
                }

                return View(clientes);
            }
            catch (Exception ex)
            {
                // Registrar el error para más detalles
                ModelState.AddModelError("", "Error al cargar la lista de clientes: " + ex.Message);
                return View(new List<Cliente>()); // Enviar una lista vacía en caso de error
            }
        }


        // GET: Get client by name
        [HttpGet]
        public IActionResult GetByClienteName(string nombre)
        {
            var cliente = _repository.GetClienteByName(nombre);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        // GET: Show create client form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create new client
        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repository.InsertNewCliente(cliente);
                return RedirectToAction("GetAllCliente");
            }
            return View(cliente);
        }

        // GET: Get client by ID
        [HttpGet]
        public IActionResult GetClienteById(int id)
        {
            var cliente = _repository.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpGet]
        public IActionResult EditClienteById(int id)
        {
            var cliente = _repository.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult EditClienteById(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var result = _repository.UpdateCliente(cliente);
                if (result)
                {
                    return RedirectToAction("GetAllCliente");
                }
                ModelState.AddModelError("", "No se pudo actualizar el Cliente.");
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult DeleteClienteById(int id)
        {
            bool isDeleted = _repository.DeleteClienteById(id);
            if (isDeleted)
            {
                return RedirectToAction("GetAllCliente");
            }
            return RedirectToAction("GetAllCleinte"); // Or handle the error appropriately
        }

        // POST: Toggle category status (Active/Inactive)
        [HttpPost]
        public IActionResult ToggleEstado(int id)
        {
            var cliente = _repository.GetClienteById(id);
            if (cliente != null)
            {
                cliente.Estado = !cliente.Estado;
                _repository.UpdateCliente(cliente);
            }
            return RedirectToAction("GetAllCliente");
        }

    }






}

