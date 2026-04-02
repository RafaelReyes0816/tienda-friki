using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class ItemCarritoService
{
    private readonly ItemCarritoRepository _repo;
    private readonly CarritoRepository _carRepo;
    private readonly ProductoRepository _prodRepo;
    public ItemCarritoService(ItemCarritoRepository repo, CarritoRepository carRepo, ProductoRepository prodRepo)
    {
        _repo = repo; _carRepo = carRepo; _prodRepo = prodRepo;
    }

    public async Task<IEnumerable<ItemCarrito>> GetAllAsync() => await _repo.GetAll();

    public async Task<ItemCarrito> CreateAsync(ItemCarritoCreateDTO dto)
    {
        var car = await _carRepo.GetById(dto.CarritoId);
        if (car == null) throw new Exception($"Carrito {dto.CarritoId} no existe");

        var prod = await _prodRepo.GetById(dto.ProductoId);
        if (prod == null) throw new Exception($"Producto {dto.ProductoId} no existe");

        var item = new ItemCarrito
        {
            Cantidad = dto.Cantidad,
            CarritoId = dto.CarritoId,
            ProductoId = dto.ProductoId
        };
        await _repo.Add(item);
        return item;
    }
}