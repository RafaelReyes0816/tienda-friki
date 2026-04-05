using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class ProductoRepository
{
    private readonly DBContext _context;
    public ProductoRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<Producto>> GetAll() => await _context.Productos.Include(p => p.Categoria).ToListAsync();
    public async Task<Producto?> GetById(int id) => await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
    public async Task Add(Producto producto) { _context.Productos.Add(producto); await _context.SaveChangesAsync(); }
    public async Task Update(Producto producto) { _context.Productos.Update(producto); await _context.SaveChangesAsync(); }
    public async Task<bool> Delete(int id)
    {
        var entity = await _context.Productos.FindAsync(id);
        if (entity == null) return false;
        _context.Productos.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Exists(int id) => await _context.Productos.AnyAsync(p => p.Id == id);
}