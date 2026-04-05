using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class DetallePedidoRepository
{
    private readonly DBContext _context;
    public DetallePedidoRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<DetallePedido>> GetAll() => await _context.DetallesPedido.Include(d => d.Producto).Include(d => d.Pedido).ToListAsync();
    public async Task<DetallePedido?> GetById(int id) => await _context.DetallesPedido.Include(d => d.Producto).Include(d => d.Pedido).FirstOrDefaultAsync(d => d.Id == id);
    public async Task Add(DetallePedido detalle) { _context.DetallesPedido.Add(detalle); await _context.SaveChangesAsync(); }
    public async Task Update(DetallePedido detalle) { _context.DetallesPedido.Update(detalle); await _context.SaveChangesAsync(); }
    public async Task<bool> Delete(int id)
    {
        var entity = await _context.DetallesPedido.FindAsync(id);
        if (entity == null) return false;
        _context.DetallesPedido.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Exists(int id) => await _context.DetallesPedido.AnyAsync(d => d.Id == id);
}