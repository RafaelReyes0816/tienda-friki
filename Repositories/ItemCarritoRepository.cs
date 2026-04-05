using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class ItemCarritoRepository
{
    private readonly DBContext _context;
    public ItemCarritoRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<ItemCarrito>> GetAll() => await _context.ItemsCarrito.Include(i => i.Producto).Include(i => i.Carrito).ToListAsync();
    public async Task<ItemCarrito?> GetById(int id) => await _context.ItemsCarrito.Include(i => i.Producto).Include(i => i.Carrito).FirstOrDefaultAsync(i => i.Id == id);
    public async Task Add(ItemCarrito item) { _context.ItemsCarrito.Add(item); await _context.SaveChangesAsync(); }
    public async Task Update(ItemCarrito item) { _context.ItemsCarrito.Update(item); await _context.SaveChangesAsync(); }
    public async Task<bool> Delete(int id)
    {
        var entity = await _context.ItemsCarrito.FindAsync(id);
        if (entity == null) return false;
        _context.ItemsCarrito.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Exists(int id) => await _context.ItemsCarrito.AnyAsync(i => i.Id == id);
}