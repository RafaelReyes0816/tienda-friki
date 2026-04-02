using tienda_friki.Models;
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

    public async Task<IEnumerable<ItemCarrito>> GetAll() => await _repo.GetAll();

    public async Task Create(ItemCarrito item)
    {
        var car = await _carRepo.GetById(item.CarritoId);
        if (car == null) throw new KeyNotFoundException($"Carrito {item.CarritoId} no existe");
        var prod = await _prodRepo.GetById(item.ProductoId);
        if (prod == null) throw new KeyNotFoundException($"Producto {item.ProductoId} no existe");
        await _repo.Add(item);
    }
}