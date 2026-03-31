using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Repositories;

namespace tienda_friki.Controllers;

[Route("api/pedidos")]
[ApiController]
public class PedidosController : ControllerBase
{
    private readonly IPedidoRepository _repo;

    public PedidosController(IPedidoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _repo.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(Pedido pedido)
    {
        await _repo.Add(pedido);
        return Ok(pedido);
    }
}
