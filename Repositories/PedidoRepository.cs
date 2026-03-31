using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public interface IPedidoRepository
{
    Task<IEnumerable<Pedido>> GetAll();
    Task Add(Pedido pedido);
}

public class PedidoRepository : IPedidoRepository
{
    private readonly DBContext _context;

    public PedidoRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pedido>> GetAll()
        => await _context.Pedidos.ToListAsync();

    public async Task Add(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
    }
}
