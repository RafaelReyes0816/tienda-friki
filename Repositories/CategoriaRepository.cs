using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class CategoriaRepository
{
    private readonly DBContext _context;
    public CategoriaRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<Categoria>> GetAll() => await _context.Categorias.ToListAsync();
    public async Task<Categoria?> GetById(int id) => await _context.Categorias.FindAsync(id);
    public async Task Add(Categoria categoria) { _context.Categorias.Add(categoria); await _context.SaveChangesAsync(); }
    public async Task Update(Categoria categoria) { _context.Categorias.Update(categoria); await _context.SaveChangesAsync(); }
    public async Task<bool> Delete(int id)
    {
        var entity = await _context.Categorias.FindAsync(id);
        if (entity == null) return false;
        _context.Categorias.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Exists(int id) => await _context.Categorias.AnyAsync(c => c.Id == id);
}