using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class CarritoService
{
    private readonly CarritoRepository _repo;
    private readonly UsuarioRepository _usrRepo;
    public CarritoService(CarritoRepository repo, UsuarioRepository usrRepo)
    {
        _repo = repo; _usrRepo = usrRepo;
    }

    public async Task<IEnumerable<Carrito>> GetAllAsync() => await _repo.GetAll();

    public async Task<Carrito> CreateAsync(CarritoCreateDTO dto)
    {
        var usr = await _usrRepo.GetById(dto.UsuarioId);
        if (usr == null) throw new Exception($"Usuario {dto.UsuarioId} no existe");

        var carrito = new Carrito
        {
            FechaCreacion = dto.FechaCreacion,
            UsuarioId = dto.UsuarioId
        };
        await _repo.Add(carrito);
        return carrito;
    }
}