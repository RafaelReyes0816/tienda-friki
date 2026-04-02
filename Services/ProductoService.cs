using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class ProductoService
{
    private readonly ProductoRepository _repo;
    private readonly CategoriaRepository _catRepo;
    public ProductoService(ProductoRepository repo, CategoriaRepository catRepo)
    {
        _repo = repo; _catRepo = catRepo;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync() => await _repo.GetAll();

    public async Task<Producto> CreateAsync(ProductoCreateDTO dto)
    {
        var cat = await _catRepo.GetById(dto.CategoriaId);
        if (cat == null) throw new Exception($"Categoría {dto.CategoriaId} no existe");

        var producto = new Producto
        {
            Nombre = dto.Nombre, Imagen = dto.Imagen, Precio = dto.Precio,
            Franquicia = dto.Franquicia, Oferta = dto.Oferta, Destacado = dto.Destacado,
            Stock = dto.Stock, CategoriaId = dto.CategoriaId
        };
        await _repo.Add(producto);
        return producto;
    }
}