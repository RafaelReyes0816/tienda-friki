using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Services;

namespace tienda_friki.Controllers;

[Route("api/carritos")]
[ApiController]
public class CarritosController : ControllerBase
{
    private readonly CarritoService _service;
    public CarritosController(CarritoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(Carrito carrito)
    {
        try { await _service.Create(carrito); return Ok(carrito); }
        catch (KeyNotFoundException ex) { return BadRequest(new { message = ex.Message }); }
    }
}