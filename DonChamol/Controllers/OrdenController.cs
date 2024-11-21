using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DonChamol.Models;
using DonChamol.Models.Dto;

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
        var clientes = _repository.GetAllCliente();
        var meseros = _repository.GetAllMeseros();
        var mesas = _repository.GetAllMesas();
        var menuItems = _repository.GetAllMenuItems();

        var model = new CreateOrdenViewModel
        {
            Clientes = clientes.Select(c => new ClienteDto { id_Cliente = c.id_Cliente, Nombre = c.Nombre }).ToList(),
            Meseros = meseros.Select(m => new MeseroDto { id_Mesero = m.id_Mesero, Nombre = m.Nombre }).ToList(),
            Mesas = mesas.Select(m => new MesaDto { id_Mesa = m.id_Mesa, Numero = m.Numero_Mesa }).ToList(),
            MenuItems = menuItems.Select(m => new MenuDto { id_Menu = m.id_Menu, Nombre = m.Nombre, Precio = m.Precio }).ToList()
        };

        return View("CreateOrden", model);
    }

}