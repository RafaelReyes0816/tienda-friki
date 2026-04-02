using System.ComponentModel.DataAnnotations;
using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _repo;
    public UsuarioService(UsuarioRepository repo) => _repo = repo;

    public async Task<IEnumerable<Usuario>> GetAll() => await _repo.GetAll();

    public async Task Create(Usuario usuario)
    {
        var dto = new UsuarioCreateDTO
        {
            Nombre = usuario.Nombre, Email = usuario.Email, Telefono = usuario.Telefono,
            Contrasena = usuario.Contrasena, Rol = usuario.Rol
        };
        Validator.ValidateObject(dto, new ValidationContext(dto), validateAllProperties: true);
        await _repo.Add(usuario);
    }
}