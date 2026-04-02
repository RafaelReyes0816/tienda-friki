using System.ComponentModel.DataAnnotations;
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
        _repo = repo;
        _catRepo = catRepo;
    }

    public async Task<IEnumerable<Producto>> GetAll() => await _repo.GetAll();

    public async Task Create(Producto producto)
    {
        var dto = new ProductoCreateDTO
        {
            Nombre = producto.Nombre, Precio = producto.Precio,
            Franquicia = producto.Franquicia, Stock = producto.Stock,
            CategoriaId = producto.CategoriaId
        };
        Validator.ValidateObject(dto, new ValidationContext(dto), validateAllProperties: true);

        var cat = await _catRepo.GetById(producto.CategoriaId);
        if (cat == null) throw new KeyNotFoundException($"Categoría {producto.CategoriaId} no existe");

        await _repo.Add(producto);
    }
}