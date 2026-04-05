using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _repo;
    public UsuarioService(UsuarioRepository repo) => _repo = repo;

    public async Task<IEnumerable<Usuario>> GetAllAsync() => await _repo.GetAll();
    public async Task<Usuario?> GetByIdAsync(int id) => await _repo.GetById(id);

    public async Task<Usuario> CreateAsync(UsuarioCreateDTO dto)
    {
        if (await _repo.GetByEmail(dto.Email) != null) throw new Exception("El email ya está registrado");
        var user = new Usuario { Nombre = dto.Nombre, Email = dto.Email, Telefono = dto.Telefono, Contrasena = dto.Contrasena, Rol = dto.Rol, FechaRegistro = DateTime.UtcNow };
        await _repo.Add(user);
        return user;
    }

    public async Task<Usuario> UpdateAsync(int id, UsuarioUpdateDTO dto)
    {
        var user = await _repo.GetById(id) ?? throw new Exception("Usuario no encontrado");
        if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != user.Email)
        {
            if (await _repo.GetByEmail(dto.Email) != null) throw new Exception("El email ya está en uso por otro usuario");
            user.Email = dto.Email;
        }
        if (!string.IsNullOrWhiteSpace(dto.Nombre)) user.Nombre = dto.Nombre;
        if (!string.IsNullOrWhiteSpace(dto.Telefono)) user.Telefono = dto.Telefono;
        if (!string.IsNullOrWhiteSpace(dto.Contrasena)) user.Contrasena = dto.Contrasena;
        if (!string.IsNullOrWhiteSpace(dto.Rol)) user.Rol = dto.Rol;
        await _repo.Update(user);
        return user;
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.Delete(id);

    public async Task<Usuario?> LoginAsync(string email, string contrasena)
    {
        var user = await _repo.GetByEmail(email);
        return (user != null && user.Contrasena == contrasena) ? user : null; // ⚠️ En prod: usar hash
    }
}