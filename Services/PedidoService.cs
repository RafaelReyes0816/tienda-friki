using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class PedidoService
{
    private readonly PedidoRepository _repo;
    private readonly UsuarioRepository _usrRepo;
    public PedidoService(PedidoRepository repo, UsuarioRepository usrRepo)
    {
        _repo = repo; _usrRepo = usrRepo;
    }

    public async Task<IEnumerable<Pedido>> GetAllAsync() => await _repo.GetAll();

    public async Task<Pedido> CreateAsync(PedidoCreateDTO dto)
    {
        var usr = await _usrRepo.GetById(dto.UsuarioId);
        if (usr == null) throw new Exception($"Usuario {dto.UsuarioId} no existe");

        var pedido = new Pedido
        {
            Codigo = dto.Codigo,
            Fecha = dto.Fecha,
            Estado = dto.Estado,
            Total = dto.Total,
            UsuarioId = dto.UsuarioId
        };
        await _repo.Add(pedido);
        return pedido;
    }
}