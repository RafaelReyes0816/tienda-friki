using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class CarritoService
{
    private readonly CarritoRepository _repo;
    private readonly UsuarioRepository _usrRepo;
    public CarritoService(CarritoRepository repo, UsuarioRepository usrRepo) { _repo = repo; _usrRepo = usrRepo; }

    public async Task<IEnumerable<Carrito>> GetAllAsync() => await _repo.GetAll();
    public async Task<Carrito?> GetByIdAsync(int id) => await _repo.GetById(id);

    public async Task<Carrito> CreateAsync(CarritoCreateDTO dto)
    {
        if (!await _usrRepo.Exists(dto.UsuarioId)) throw new Exception($"Usuario {dto.UsuarioId} no existe");
        var carrito = new Carrito { FechaCreacion = dto.FechaCreacion, UsuarioId = dto.UsuarioId };
        await _repo.Add(carrito);
        return carrito;
    }

    public async Task<Carrito> UpdateAsync(int id, CarritoUpdateDTO dto)
    {
        var carrito = await _repo.GetById(id) ?? throw new Exception("Carrito no encontrado");
        if (dto.UsuarioId.HasValue && !await _usrRepo.Exists(dto.UsuarioId.Value)) throw new Exception("Usuario no existe");
        if (dto.UsuarioId.HasValue) carrito.UsuarioId = dto.UsuarioId.Value;
        if (dto.FechaCreacion.HasValue) carrito.FechaCreacion = dto.FechaCreacion.Value;
        await _repo.Update(carrito);
        return carrito;
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.Delete(id);
}