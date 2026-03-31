using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Repositories;

namespace tienda_friki.Controllers;

[Route("api/carritos")]
[ApiController]
public class CarritosController : ControllerBase
{
    private readonly ICarritoRepository _repo;

    public CarritosController(ICarritoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _repo.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(Carrito carrito)
    {
        await _repo.Add(carrito);
        return Ok(carrito);
    }
}
