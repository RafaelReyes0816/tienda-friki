using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class DetallePedidoService
{
    private readonly DetallePedidoRepository _repo;
    private readonly PedidoRepository _pedRepo;
    private readonly ProductoRepository _prodRepo;
    public DetallePedidoService(DetallePedidoRepository repo, PedidoRepository pedRepo, ProductoRepository prodRepo) { _repo = repo; _pedRepo = pedRepo; _prodRepo = prodRepo; }

    public async Task<IEnumerable<DetallePedido>> GetAllAsync() => await _repo.GetAll();
    public async Task<DetallePedido?> GetByIdAsync(int id) => await _repo.GetById(id);

    public async Task<DetallePedido> CreateAsync(DetallePedidoCreateDTO dto)
    {
        if (!await _pedRepo.Exists(dto.PedidoId)) throw new Exception("Pedido no existe");
        if (!await _prodRepo.Exists(dto.ProductoId)) throw new Exception("Producto no existe");
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

    public async Task<DetallePedido> UpdateAsync(int id, DetallePedidoUpdateDTO dto)
    {
        var detalle = await _repo.GetById(id) ?? throw new Exception("Detalle no encontrado");
        if (dto.PedidoId.HasValue && !await _pedRepo.Exists(dto.PedidoId.Value)) throw new Exception("Pedido no existe");
        if (dto.ProductoId.HasValue && !await _prodRepo.Exists(dto.ProductoId.Value)) throw new Exception("Producto no existe");
        if (dto.Cantidad.HasValue) detalle.Cantidad = dto.Cantidad.Value;
        if (dto.PrecioUnitario.HasValue) detalle.PrecioUnitario = dto.PrecioUnitario.Value;
        if (dto.Subtotal.HasValue) detalle.Subtotal = dto.Subtotal.Value;
        if (dto.PedidoId.HasValue) detalle.PedidoId = dto.PedidoId.Value;
        if (dto.ProductoId.HasValue) detalle.ProductoId = dto.ProductoId.Value;
        await _repo.Update(detalle);
        return detalle;
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.Delete(id);
}