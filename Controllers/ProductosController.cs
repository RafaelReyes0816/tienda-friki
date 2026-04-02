using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Services;

namespace tienda_friki.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly ProductoService _service;
    public ProductosController(ProductoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductoCreateDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
        catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
    }
}