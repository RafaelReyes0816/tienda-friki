using tienda_friki.Models;
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

    public async Task<IEnumerable<Pedido>> GetAll() => await _repo.GetAll();

    public async Task Create(Pedido pedido)
    {
        var usr = await _usrRepo.GetById(pedido.UsuarioId);
        if (usr == null) throw new KeyNotFoundException($"Usuario {pedido.UsuarioId} no existe");
        await _repo.Add(pedido);
    }
}