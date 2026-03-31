using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Repositories;

var builder = WebApplication.CreateBuilder(args);

// EF: una instancia de DBContext por petición HTTP (Scoped); usa la cadena de appsettings.
builder.Services.AddDbContext<DBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI: al pedir I...Repository en un controlador, se inyecta la clase concreta.
// Scoped = misma instancia durante toda la petición.
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IItemCarritoRepository, ItemCarritoRepository>();
builder.Services.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); // rutas de Controllers

app.Run();
