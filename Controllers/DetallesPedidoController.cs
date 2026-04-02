using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Services;

namespace tienda_friki.Controllers;

[Route("api/detalles-pedido")]
[ApiController]
public class DetallesPedidoController : ControllerBase
{
    private readonly DetallePedidoService _service;
    public DetallesPedidoController(DetallePedidoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(DetallePedido detalle)
    {
        try { await _service.Create(detalle); return Ok(detalle); }
        catch (KeyNotFoundException ex) { return BadRequest(new { message = ex.Message }); }
    }
}