using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAll();
    Task Add(Usuario usuario);
}

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DBContext _context;

    public UsuarioRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAll()
        => await _context.Usuarios.ToListAsync();

    public async Task Add(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }
}
