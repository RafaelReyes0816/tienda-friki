using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class CarritoRepository
{
    private readonly DBContext _context;

    public CarritoRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<Carrito>> GetAll()
        => await _context.Carritos.Include(c => c.Usuario).ToListAsync();

    public async Task<Carrito?> GetById(int id)
        => await _context.Carritos.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.Id == id);

    public async Task Add(Carrito carrito)
    {
        _context.Carritos.Add(carrito);
        await _context.SaveChangesAsync();
    }
}