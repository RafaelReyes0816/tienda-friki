using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Repositories;

namespace tienda_friki.Controllers;

[Route("api/detalles-pedido")]
[ApiController]
public class DetallesPedidoController : ControllerBase
{
    private readonly IDetallePedidoRepository _repo;

    public DetallesPedidoController(IDetallePedidoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _repo.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(DetallePedido detalle)
    {
        await _repo.Add(detalle);
        return Ok(detalle);
    }
}
