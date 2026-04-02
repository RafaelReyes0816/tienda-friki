using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Services;

namespace tienda_friki.Controllers;

[Route("api/usuarios")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;
    public UsuariosController(UsuarioService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(Usuario usuario)
    {
        await _service.Create(usuario);
        return Ok(usuario);
    }
}