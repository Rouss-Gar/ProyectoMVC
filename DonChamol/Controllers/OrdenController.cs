using DonChamol.Models;
using Microsoft.AspNetCore.Mvc;

public class OrdenController : Controller
{
    private readonly OrdenRepository _repository = new OrdenRepository();

    [HttpGet]
    public IActionResult GetAllOrdenes()
    {
        var GetAllOrden = _repository.GetAllOrden();
        return View(GetAllOrden);
    }

    [HttpGet]
    public IActionResult Create()
    {



        var clientes = _repository.GetAllCliente()
            .Select(c => new { c.id_Cliente, c.Nombre }).ToList();
        var meseros = _repository.GetAllMeseros()
            .Select(m => new { m.id_Mesero, m.Nombre }).ToList();
        var mesas = _repository.GetAllMesas()
            .Select(m => new { m.id_Mesa, Numero = m.Numero_Mesa }).ToList();
        var menuItems = _repository.GetAllMenuItems()
            .Select(m => new { m.id_Menu, m.Nombre, m.Precio }).ToList();

        if (!clientes.Any() || !meseros.Any() || !mesas.Any() || !menuItems.Any())
        {
            TempData["Error"] = "Datos insuficientes para crear una orden. Verifica que haya clientes, meseros, mesas y menús disponibles.";
            return RedirectToAction("GetAllOrdenes");
        }

        ViewBag.Cliente = clientes;
        ViewBag.Meseros = meseros;
        ViewBag.Mesas = mesas;
        ViewBag.MenuItems = menuItems;

        return View(new Orden());
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Orden nuevaOrden, List<DetalleOrden> detalles)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Cliente = _repository.GetAllCliente()
                ?.Select(c => new Cliente { id_Cliente = c.id_Cliente, Nombre = c.Nombre }).ToList()
                ?? new List<Cliente>();

            ViewBag.Meseros = _repository.GetAllMeseros()
                ?.Select(m => new Meseros { id_Mesero = m.id_Mesero, Nombre = m.Nombre }).ToList()
                ?? new List<Meseros>();

            ViewBag.Mesas = _repository.GetAllMesas()
                ?.Select(m => new Mesas { id_Mesa = m.id_Mesa, Numero_Mesa = m.Numero_Mesa }).ToList()
                ?? new List<Mesas>();

            ViewBag.MenuItems = _repository.GetAllMenuItems()
                ?.Select(m => new MenuItems { id_Menu = m.id_Menu, Nombre = m.Nombre, Precio = m.Precio }).ToList()
                ?? new List<MenuItems>();

            return View(nuevaOrden);
        }

        try
        {
            bool isInserted = _repository.InsertNewOrden(nuevaOrden, detalles);

            if (isInserted)
            {
                TempData["Success"] = "Orden creada exitosamente.";
            }
            else
            {
                TempData["Error"] = "No se pudo crear la orden. Inténtelo nuevamente.";
                return View(nuevaOrden);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al guardar la orden: {ex.Message}";
            return View(nuevaOrden);
        }

        return RedirectToAction("GetAllOrdenes");
    }


    [HttpDelete("{idOrden}")]
    public IActionResult EliminarOrden(int idOrden)
    {
        if (idOrden <= 0)
        {
            return BadRequest("El ID de la orden debe ser un número positivo.");
        }

        bool resultado = _repository.DeleteOrdenById(idOrden);

        if (resultado)
        {
            return RedirectToAction("GetAllOrdenes");
        }

        return StatusCode(500, new { mensaje = "No se pudo eliminar la orden. Por favor, intente nuevamente." });
    }

}