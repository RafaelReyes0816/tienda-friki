using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class CategoriaService
{
    private readonly CategoriaRepository _repo;
    public CategoriaService(CategoriaRepository repo) => _repo = repo;

    public async Task<IEnumerable<Categoria>> GetAllAsync() => await _repo.GetAll();

    public async Task<Categoria> CreateAsync(CategoriaCreateDTO dto)
    {
        var categoria = new Categoria { Nombre = dto.Nombre };
        await _repo.Add(categoria);
        return categoria;
    }
}