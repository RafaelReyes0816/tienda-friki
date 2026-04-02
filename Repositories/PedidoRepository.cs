using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class PedidoRepository
{
    private readonly DBContext _context;

    public PedidoRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<Pedido>> GetAll()
        => await _context.Pedidos.Include(p => p.Usuario).ToListAsync();

    public async Task<Pedido?> GetById(int id)
        => await _context.Pedidos.Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Id == id);

    public async Task Add(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
    }
}