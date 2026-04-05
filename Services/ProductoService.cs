using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class ProductoService
{
    private readonly ProductoRepository _repo;
    private readonly CategoriaRepository _catRepo;
    public ProductoService(ProductoRepository repo, CategoriaRepository catRepo) { _repo = repo; _catRepo = catRepo; }

    public async Task<IEnumerable<Producto>> GetAllAsync() => await _repo.GetAll();
    public async Task<Producto?> GetByIdAsync(int id) => await _repo.GetById(id);

    public async Task<Producto> CreateAsync(ProductoCreateDTO dto)
    {
        if (!await _catRepo.Exists(dto.CategoriaId)) throw new Exception("Categoría no existe");
        if (dto.Stock < 0) throw new Exception("El stock no puede ser negativo");
        var prod = new Producto { Nombre = dto.Nombre, Imagen = dto.Imagen, Precio = dto.Precio, Franquicia = dto.Franquicia, Oferta = dto.Oferta, Destacado = dto.Destacado, Stock = dto.Stock, CategoriaId = dto.CategoriaId };
        await _repo.Add(prod);
        return prod;
    }

    public async Task<Producto> UpdateAsync(int id, ProductoUpdateDTO dto)
    {
        var prod = await _repo.GetById(id) ?? throw new Exception("Producto no encontrado");
        if (dto.CategoriaId.HasValue && !await _catRepo.Exists(dto.CategoriaId.Value)) throw new Exception("Categoría no existe");
        if (dto.Stock.HasValue && dto.Stock.Value < 0) throw new Exception("El stock no puede ser negativo");
        if (!string.IsNullOrWhiteSpace(dto.Nombre)) prod.Nombre = dto.Nombre;
        if (dto.Imagen != null) prod.Imagen = dto.Imagen;
        if (dto.Precio.HasValue) prod.Precio = dto.Precio.Value;
        if (!string.IsNullOrWhiteSpace(dto.Franquicia)) prod.Franquicia = dto.Franquicia;
        if (dto.Oferta.HasValue) prod.Oferta = dto.Oferta.Value;
        if (dto.Destacado.HasValue) prod.Destacado = dto.Destacado.Value;
        if (dto.Stock.HasValue) prod.Stock = dto.Stock.Value;
        if (dto.CategoriaId.HasValue) prod.CategoriaId = dto.CategoriaId.Value;
        await _repo.Update(prod);
        return prod;
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.Delete(id);
}