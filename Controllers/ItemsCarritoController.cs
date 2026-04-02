using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Services;

namespace tienda_friki.Controllers;

[Route("api/items-carrito")]
[ApiController]
public class ItemsCarritoController : ControllerBase
{
    private readonly ItemCarritoService _service;
    public ItemsCarritoController(ItemCarritoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(ItemCarrito item)
    {
        try { await _service.Create(item); return Ok(item); }
        catch (KeyNotFoundException ex) { return BadRequest(new { message = ex.Message }); }
    }
}