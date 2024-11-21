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
            return RedirectToAction("Index");
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

        // Aquí tu lógica para guardar la orden
        TempData["Success"] = "Orden creada exitosamente.";
        return RedirectToAction("GetAllOrdenes");
    }



}