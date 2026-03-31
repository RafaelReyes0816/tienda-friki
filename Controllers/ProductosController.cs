using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Repositories;

namespace tienda_friki.Controllers;

[Route("api/productos")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repo;

    public ProductosController(IProductoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _repo.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(Producto producto)
    {
        await _repo.Add(producto);
        return Ok(producto);
    }
}
