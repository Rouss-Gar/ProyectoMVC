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
    public IActionResult Create(Orden nuevaOrden)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Cliente = _repository.GetAllCliente()
                .Select(c => new { c.id_Cliente, c.Nombre }).ToList();
            ViewBag.Meseros = _repository.GetAllMeseros()
                .Select(m => new { m.id_Mesero, m.Nombre }).ToList();
            ViewBag.Mesas = _repository.GetAllMesas()
                .Select(m => new { m.id_Mesa, Numero = m.Numero_Mesa }).ToList();
            ViewBag.MenuItems = _repository.GetAllMenuItems()
                .Select(m => new { m.id_Menu, m.Nombre, m.Precio }).ToList();

            return View(nuevaOrden);
        }

        try
        {
            _repository.InsertNewOrden(nuevaOrden); // Llama al método para guardar la orden
            TempData["Success"] = "Orden creada exitosamente.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al guardar la orden: {ex.Message}";
            return View(nuevaOrden); // Si hay error, regresa a la vista con la orden
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