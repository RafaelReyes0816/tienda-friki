using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;
using tienda_friki.Services;

namespace tienda_friki.Controllers;

[Route("api/categorias")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly CategoriaService _service;
    public CategoriasController(CategoriaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAll());

    [HttpPost]
    public async Task<IActionResult> Post(Categoria categoria)
    {
        await _service.Create(categoria);
        return Ok(categoria);
    }
}