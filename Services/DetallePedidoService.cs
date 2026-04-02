using tienda_friki.Models;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class DetallePedidoService
{
    private readonly DetallePedidoRepository _repo;
    private readonly PedidoRepository _pedRepo;
    private readonly ProductoRepository _prodRepo;

    public DetallePedidoService(DetallePedidoRepository repo, PedidoRepository pedRepo, ProductoRepository prodRepo)
    {
        _repo = repo; _pedRepo = pedRepo; _prodRepo = prodRepo;
    }

    public async Task<IEnumerable<DetallePedido>> GetAll() => await _repo.GetAll();

    public async Task Create(DetallePedido detalle)
    {
        var ped = await _pedRepo.GetById(detalle.PedidoId);
        if (ped == null) throw new KeyNotFoundException($"Pedido {detalle.PedidoId} no existe");
        var prod = await _prodRepo.GetById(detalle.ProductoId);
        if (prod == null) throw new KeyNotFoundException($"Producto {detalle.ProductoId} no existe");

        if (detalle.Subtotal == 0)
            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

        await _repo.Add(detalle);
    }
}