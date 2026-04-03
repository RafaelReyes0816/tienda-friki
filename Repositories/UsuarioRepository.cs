using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class UsuarioRepository
{
    private readonly DBContext _context;

    public UsuarioRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<Usuario>> GetAll()
        => await _context.Usuarios.ToListAsync();

    public async Task<Usuario?> GetById(int id)
        => await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<Usuario?> GetByEmail(string email)
        => await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

    public async Task Add(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }
}