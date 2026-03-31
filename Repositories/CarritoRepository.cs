using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public interface ICarritoRepository
{
    Task<IEnumerable<Carrito>> GetAll();
    Task Add(Carrito carrito);
}

public class CarritoRepository : ICarritoRepository
{
    private readonly DBContext _context;

    public CarritoRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Carrito>> GetAll()
        => await _context.Carritos.ToListAsync();

    public async Task Add(Carrito carrito)
    {
        _context.Carritos.Add(carrito);
        await _context.SaveChangesAsync();
    }
}
