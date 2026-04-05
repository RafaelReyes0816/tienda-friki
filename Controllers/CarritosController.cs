using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models.DTOs;
using tienda_friki.Services;

namespace tienda_friki.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarritosController : ControllerBase
{
    private readonly CarritoService _service;
    public CarritosController(CarritoService service) => _service = service;

    [HttpGet] public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());
    [HttpGet("{id}")] public async Task<IActionResult> GetById(int id)
    {
        var r = await _service.GetByIdAsync(id);
        return r == null ? NotFound() : Ok(r);
    }
    [HttpPost] public async Task<IActionResult> Post([FromBody] CarritoCreateDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return Ok(await _service.CreateAsync(dto)); }
        catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
    }
    [HttpPut("{id}")] public async Task<IActionResult> Put(int id, [FromBody] CarritoUpdateDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return Ok(await _service.UpdateAsync(id, dto)); }
        catch (Exception ex) { return NotFound(new { message = ex.Message }); }
    }
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id)
    {
        try { return await _service.DeleteAsync(id) ? NoContent() : NotFound(new { message = "No encontrado" }); }
        catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
    }
}