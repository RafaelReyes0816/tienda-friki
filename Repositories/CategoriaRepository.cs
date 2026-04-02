using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class CategoriaRepository
{
    private readonly DBContext _context;

    public CategoriaRepository(DBContext context) => _context = context;

    public async Task<IEnumerable<Categoria>> GetAll()
        => await _context.Categorias.ToListAsync();

    public async Task<Categoria?> GetById(int id)
        => await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

    public async Task Add(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
    }
}