using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class ProductoRepository
{
    private readonly DBContext _context;

    public ProductoRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<Producto>> GetAll()
        => await _context.Productos.Include(p => p.Categoria).ToListAsync();

    public async Task<Producto?> GetById(int id)
        => await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);

    public async Task Add(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
    }
}