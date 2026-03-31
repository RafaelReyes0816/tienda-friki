using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> GetAll();
    Task Add(Producto producto);
}

public class ProductoRepository : IProductoRepository
{
    private readonly DBContext _context;

    public ProductoRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Producto>> GetAll()
        => await _context.Productos.ToListAsync();

    public async Task Add(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
    }
}
