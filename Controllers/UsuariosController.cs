using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Repositories;

namespace tienda_friki.Controllers;

[Route("api/usuarios")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioRepository _repo;

    public UsuariosController(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _repo.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(Usuario usuario)
    {
        await _repo.Add(usuario);
        return Ok(usuario);
    }
}
