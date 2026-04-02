using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Models.DTOs;
using tienda_friki.Services;

namespace tienda_friki.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;
    public UsuariosController(UsuarioService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UsuarioCreateDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }
}