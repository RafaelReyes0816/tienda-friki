using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _repo;
    public UsuarioService(UsuarioRepository repo) => _repo = repo;

    public async Task<IEnumerable<Usuario>> GetAllAsync() => await _repo.GetAll();

    public async Task<Usuario> CreateAsync(UsuarioCreateDTO dto)
    {
        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            Telefono = dto.Telefono,
            Contrasena = dto.Contrasena,
            Rol = dto.Rol,
            FechaRegistro = DateTime.UtcNow 
        };
        
        await _repo.Add(usuario);
        return usuario;
    }
}