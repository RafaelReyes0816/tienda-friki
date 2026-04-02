using tienda_friki.Models;
using tienda_friki.Models.DTOs;
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

    public async Task<IEnumerable<DetallePedido>> GetAllAsync() => await _repo.GetAll();

    public async Task<DetallePedido> CreateAsync(DetallePedidoCreateDTO dto)
    {
        var ped = await _pedRepo.GetById(dto.PedidoId);
        if (ped == null) throw new Exception($"Pedido {dto.PedidoId} no existe");

        var prod = await _prodRepo.GetById(dto.ProductoId);
        if (prod == null) throw new Exception($"Producto {dto.ProductoId} no existe");

        var detalle = new DetallePedido
        {
            Cantidad = dto.Cantidad,
            PrecioUnitario = dto.PrecioUnitario,
            Subtotal = dto.Subtotal == 0 ? dto.Cantidad * dto.PrecioUnitario : dto.Subtotal,
            PedidoId = dto.PedidoId,
            ProductoId = dto.ProductoId
        };
        await _repo.Add(detalle);
        return detalle;
    }
}