using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class CategoriaService
{
    private readonly CategoriaRepository _repo;
    public CategoriaService(CategoriaRepository repo) => _repo = repo;

    public async Task<IEnumerable<Categoria>> GetAllAsync() => await _repo.GetAll();
    public async Task<Categoria?> GetByIdAsync(int id) => await _repo.GetById(id);

    public async Task<Categoria> CreateAsync(CategoriaCreateDTO dto)
    {
        var cat = new Categoria { Nombre = dto.Nombre };
        await _repo.Add(cat);
        return cat;
    }

    public async Task<Categoria> UpdateAsync(int id, CategoriaUpdateDTO dto)
    {
        var cat = await _repo.GetById(id) ?? throw new Exception("Categoría no encontrada");
        if (!string.IsNullOrWhiteSpace(dto.Nombre)) cat.Nombre = dto.Nombre;
        await _repo.Update(cat);
        return cat;
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.Delete(id);
}