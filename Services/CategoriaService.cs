using System.ComponentModel.DataAnnotations;
using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class CategoriaService
{
    private readonly CategoriaRepository _repo;

    public CategoriaService(CategoriaRepository repo) => _repo = repo;

    public async Task<IEnumerable<Categoria>> GetAll() => await _repo.GetAll();

    public async Task Create(Categoria categoria)
    {
        // Validación interna con DTO sin cambiar firma del servicio
        var dto = new CategoriaCreateDTO { Nombre = categoria.Nombre };
        var ctx = new ValidationContext(dto);
        Validator.ValidateObject(dto, ctx, validateAllProperties: true);

        await _repo.Add(categoria);
    }
}