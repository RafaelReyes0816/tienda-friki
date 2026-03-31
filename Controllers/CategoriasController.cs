using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Repositories;

namespace tienda_friki.Controllers;

[Route("api/categorias")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _repo;

    public CategoriasController(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _repo.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(Categoria categoria)
    {
        await _repo.Add(categoria);
        return Ok(categoria);
    }
}
