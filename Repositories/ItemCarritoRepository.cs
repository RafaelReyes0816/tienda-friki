using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public interface IItemCarritoRepository
{
    Task<IEnumerable<ItemCarrito>> GetAll();
    Task Add(ItemCarrito item);
}

public class ItemCarritoRepository : IItemCarritoRepository
{
    private readonly DBContext _context;

    public ItemCarritoRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ItemCarrito>> GetAll()
        => await _context.ItemsCarrito.ToListAsync();

    public async Task Add(ItemCarrito item)
    {
        _context.ItemsCarrito.Add(item);
        await _context.SaveChangesAsync();
    }
}
