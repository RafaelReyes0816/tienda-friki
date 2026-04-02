using tienda_friki.Models;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class CarritoService
{
    private readonly CarritoRepository _repo;
    private readonly UsuarioRepository _usrRepo;
    public CarritoService(CarritoRepository repo, UsuarioRepository usrRepo)
    {
        _repo = repo;
        _usrRepo = usrRepo;
    }

    public async Task<IEnumerable<Carrito>> GetAll() => await _repo.GetAll();

    public async Task Create(Carrito carrito)
    {
        var usr = await _usrRepo.GetById(carrito.UsuarioId);
        if (usr == null) throw new KeyNotFoundException($"Usuario {carrito.UsuarioId} no existe");
        await _repo.Add(carrito);
    }
}