using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class ItemCarritoService
{
    private readonly ItemCarritoRepository _repo;
    private readonly CarritoRepository _carRepo;
    private readonly ProductoRepository _prodRepo;
    public ItemCarritoService(ItemCarritoRepository repo, CarritoRepository carRepo, ProductoRepository prodRepo) { _repo = repo; _carRepo = carRepo; _prodRepo = prodRepo; }

    public async Task<IEnumerable<ItemCarrito>> GetAllAsync() => await _repo.GetAll();
    public async Task<ItemCarrito?> GetByIdAsync(int id) => await _repo.GetById(id);

    public async Task<ItemCarrito> CreateAsync(ItemCarritoCreateDTO dto)
    {
        if (!await _carRepo.Exists(dto.CarritoId)) throw new Exception("Carrito no existe");
        if (!await _prodRepo.Exists(dto.ProductoId)) throw new Exception("Producto no existe");
        var item = new ItemCarrito { Cantidad = dto.Cantidad, CarritoId = dto.CarritoId, ProductoId = dto.ProductoId };
        await _repo.Add(item);
        return item;
    }

    public async Task<ItemCarrito> UpdateAsync(int id, ItemCarritoUpdateDTO dto)
    {
        var item = await _repo.GetById(id) ?? throw new Exception("Item no encontrado");
        if (dto.CarritoId.HasValue && !await _carRepo.Exists(dto.CarritoId.Value)) throw new Exception("Carrito no existe");
        if (dto.ProductoId.HasValue && !await _prodRepo.Exists(dto.ProductoId.Value)) throw new Exception("Producto no existe");
        if (dto.Cantidad.HasValue) item.Cantidad = dto.Cantidad.Value;
        if (dto.CarritoId.HasValue) item.CarritoId = dto.CarritoId.Value;
        if (dto.ProductoId.HasValue) item.ProductoId = dto.ProductoId.Value;
        await _repo.Update(item);
        return item;
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.Delete(id);
}