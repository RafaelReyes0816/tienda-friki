using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Repositories;

namespace tienda_friki.Controllers;

[Route("api/items-carrito")]
[ApiController]
public class ItemsCarritoController : ControllerBase
{
    private readonly IItemCarritoRepository _repo;

    public ItemsCarritoController(IItemCarritoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _repo.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(ItemCarrito item)
    {
        await _repo.Add(item);
        return Ok(item);
    }
}
