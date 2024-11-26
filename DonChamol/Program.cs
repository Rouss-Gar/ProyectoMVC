using DonChamol.Models;
using DonChamol.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICategoriaRepository<Categoria>, CategoriaRepository>();

builder.Services.AddScoped<IClienteRepository<Cliente>, ClienteRepository>();

builder.Services.AddScoped<IMesasRepository<Mesas>, MesaRepository>();

builder.Services.AddScoped<IMenuItemsRepository<MenuItems>, MenuItemsRepository>();

builder.Services.AddScoped<IMeserosRepository<Meseros>, MeserosRepository>();

builder.Services.AddScoped<IPagoRepository<Pago>, PagoRepository>();

builder.Services.AddScoped<IOrdenRepository<Orden>, OrdenRepository>();

builder.Services.AddScoped<IProducto<Producto>, ProductoRepository>();  

builder.Services.AddScoped<IProveedor<Proveedor>, ProveedorRepository>();   




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
