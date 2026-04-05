using Microsoft.AspNetCore.Mvc;
using tienda_friki.Models;           // ✅ Agregado para resolver el error CS0246
using tienda_friki.Models.DTOs;
using tienda_friki.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace tienda_friki.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;
    private readonly IConfiguration _config;

    public UsuariosController(UsuarioService service, IConfiguration config) 
    { 
        _service = service; 
        _config = config; 
    }

    [HttpGet] 
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")] 
    public async Task<IActionResult> GetById(int id) 
    {
        var r = await _service.GetByIdAsync(id);
        return r == null ? NotFound() : Ok(r);
    }

    [HttpPost] 
    public async Task<IActionResult> Post([FromBody] UsuarioCreateDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return Ok(await _service.CreateAsync(dto)); }
        catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
    }

    [HttpPut("{id}")] 
    public async Task<IActionResult> Put(int id, [FromBody] UsuarioUpdateDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return Ok(await _service.UpdateAsync(id, dto)); }
        catch (Exception ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("{id}")] 
    public async Task<IActionResult> Delete(int id)
    {
        try { return await _service.DeleteAsync(id) ? NoContent() : NotFound(new { message = "Usuario no encontrado" }); }
        catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
    {
        var usuario = await _service.LoginAsync(dto.Email, dto.Contrasena);
        if (usuario == null) return Unauthorized("Credenciales inválidas");

        return Ok(new 
        { 
            Token = GenerateJwtToken(usuario), 
            Usuario = new { usuario.Id, usuario.Nombre, usuario.Email, usuario.Rol } 
        });
    }

    private string GenerateJwtToken(Usuario usuario)
    {
        var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key not configured");
        var jwtIssuer = _config["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer not configured");
        var jwtAudience = _config["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience not configured");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Role, usuario.Rol)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(jwtIssuer, jwtAudience, claims, expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}