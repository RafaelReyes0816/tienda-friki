using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Services;

namespace tienda_friki.Controllers;

[Route("api/pedidos")]
[ApiController]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _service;
    public PedidosController(PedidoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(Pedido pedido)
    {
        try { await _service.Create(pedido); return Ok(pedido); }
        catch (KeyNotFoundException ex) { return BadRequest(new { message = ex.Message }); }
    }
}