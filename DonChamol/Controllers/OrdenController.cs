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
        ViewBag.Cliente = _repository.GetAllCliente()
            .Select(c => new { c.id_Cliente, c.Nombre }).ToList();
        ViewBag.Meseros = _repository.GetAllMeseros()
            .Select(m => new { m.id_Mesero, m.Nombre }).ToList();
        ViewBag.Mesas = _repository.GetAllMesas()
            .Select(m => new { m.id_Mesa, Numero = m.Numero_Mesa }).ToList();
        ViewBag.MenuItems = _repository.GetAllMenuItems()
            .Select(m => new { m.id_Menu, Nombre = m.Nombre, precio = m.Precio }).ToList();
        return View(new Orden());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Orden nuevaOrden)
    {
        if (!ModelState.IsValid)
        {
            // Si hay errores de validación, recarga las listas de clientes, meseros y mesas
            // y muestra la vista nuevamente con la orden parcialmente completada.
            ViewBag.Cliente = _repository.GetAllCliente()
                .Select(c => new { c.id_Cliente, c.Nombre }).ToList();
            ViewBag.Meseros = _repository.GetAllMeseros()
                .Select(m => new { m.id_Mesero, m.Nombre }).ToList();
            ViewBag.Mesas = _repository.GetAllMesas()
                .Select(m => new { m.id_Mesa, Numero = m.Numero_Mesa }).ToList();

            return View(nuevaOrden); // Regresa a la vista con los datos del modelo
        }

        // Aquí agregas los detalles de la orden si es necesario
        // En este ejemplo, supongo que quieres manejar solo la creación de la orden, pero si tienes detalles, agrégales en el método de Insert.

        bool resultado = _repository.InsertOrden(nuevaOrden, new List<OrdenDetalle>()); // Suponiendo que no hay detalles de orden, pero si los tienes, los pasas aquí.

        if (resultado)
        {
            TempData["Success"] = "La orden se creó exitosamente.";
            return RedirectToAction("Orden"); // Redirige al listado de órdenes (GetAllOrden o el que tengas para listar todas las órdenes)
        }

        // Si ocurre un error al guardar, muestra un mensaje de error.
        TempData["Error"] = "Ocurrió un error al guardar la orden. Inténtalo de nuevo.";
        return View(nuevaOrden); // Vuelve a la vista con la orden y un mensaje de error
    }



}