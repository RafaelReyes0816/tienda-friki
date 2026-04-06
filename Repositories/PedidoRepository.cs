using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class PedidoRepository
{
    private readonly DBContext _context;
    public PedidoRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<Pedido>> GetAll() => await _context.Pedidos.Include(p => p.Usuario).ToListAsync();
    public async Task<Pedido?> GetById(int id) => await _context.Pedidos.Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Id == id);
    public async Task<IEnumerable<Pedido>> GetByUsuario(int usuarioId) => await _context.Pedidos.Include(p => p.Usuario).Where(p => p.UsuarioId == usuarioId).ToListAsync();
    public async Task Add(Pedido pedido) { _context.Pedidos.Add(pedido); await _context.SaveChangesAsync(); }
    public async Task Update(Pedido pedido) { _context.Pedidos.Update(pedido); await _context.SaveChangesAsync(); }
    public async Task<bool> Delete(int id)
    {
        var entity = await _context.Pedidos.FindAsync(id);
        if (entity == null) return false;
        _context.Pedidos.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Exists(int id) => await _context.Pedidos.AnyAsync(p => p.Id == id);
}