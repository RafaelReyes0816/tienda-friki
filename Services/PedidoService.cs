using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class PedidoService
{
    private readonly PedidoRepository _repo;
    private readonly UsuarioRepository _usrRepo;
    public PedidoService(PedidoRepository repo, UsuarioRepository usrRepo) { _repo = repo; _usrRepo = usrRepo; }

    public async Task<IEnumerable<Pedido>> GetAllAsync() => await _repo.GetAll();
    public async Task<Pedido?> GetByIdAsync(int id) => await _repo.GetById(id);

    public async Task<Pedido> CreateAsync(PedidoCreateDTO dto)
    {
        if (!await _usrRepo.Exists(dto.UsuarioId)) throw new Exception("Usuario no existe");
        var pedido = new Pedido { Codigo = dto.Codigo, Fecha = dto.Fecha, Estado = dto.Estado, Total = dto.Total, UsuarioId = dto.UsuarioId };
        await _repo.Add(pedido);
        return pedido;
    }

    public async Task<Pedido> UpdateAsync(int id, PedidoUpdateDTO dto)
    {
        var pedido = await _repo.GetById(id) ?? throw new Exception("Pedido no encontrado");
        if (dto.UsuarioId.HasValue && !await _usrRepo.Exists(dto.UsuarioId.Value)) throw new Exception("Usuario no existe");
        if (!string.IsNullOrWhiteSpace(dto.Codigo)) pedido.Codigo = dto.Codigo;
        if (dto.Fecha.HasValue) pedido.Fecha = dto.Fecha.Value;
        if (!string.IsNullOrWhiteSpace(dto.Estado)) pedido.Estado = dto.Estado;
        if (dto.Total.HasValue) pedido.Total = dto.Total.Value;
        if (dto.UsuarioId.HasValue) pedido.UsuarioId = dto.UsuarioId.Value;
        await _repo.Update(pedido);
        return pedido;
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.Delete(id);
}