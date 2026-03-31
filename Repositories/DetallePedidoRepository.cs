using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public interface IDetallePedidoRepository
{
    Task<IEnumerable<DetallePedido>> GetAll();
    Task Add(DetallePedido detalle);
}

public class DetallePedidoRepository : IDetallePedidoRepository
{
    private readonly DBContext _context;

    public DetallePedidoRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DetallePedido>> GetAll()
        => await _context.DetallesPedido.ToListAsync();

    public async Task Add(DetallePedido detalle)
    {
        _context.DetallesPedido.Add(detalle);
        await _context.SaveChangesAsync();
    }
}
