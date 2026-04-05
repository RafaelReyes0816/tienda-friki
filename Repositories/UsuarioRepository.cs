using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class UsuarioRepository
{
    private readonly DBContext _context;
    public UsuarioRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<Usuario>> GetAll() => await _context.Usuarios.ToListAsync();
    public async Task<Usuario?> GetById(int id) => await _context.Usuarios.FindAsync(id);
    public async Task<Usuario?> GetByEmail(string email) => await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    public async Task Add(Usuario usuario) { _context.Usuarios.Add(usuario); await _context.SaveChangesAsync(); }
    public async Task Update(Usuario usuario) { _context.Usuarios.Update(usuario); await _context.SaveChangesAsync(); }
    public async Task<bool> Delete(int id)
    {
        var entity = await _context.Usuarios.FindAsync(id);
        if (entity == null) return false;
        _context.Usuarios.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Exists(int id) => await _context.Usuarios.AnyAsync(u => u.Id == id);
}