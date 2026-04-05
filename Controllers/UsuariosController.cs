using Microsoft.AspNetCore.Authorization;
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
    private readonly TokenService _tokens;

    public UsuariosController(UsuarioService service, TokenService tokens)
    {
        _service = service;
        _tokens = tokens;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UsuarioCreateDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
    {
        var usuario = await _service.LoginAsync(dto.Email, dto.Contrasena);
        if (usuario == null) return Unauthorized("Credenciales inválidas");

        var accessToken = _tokens.CreateAccessToken(usuario);
        var refreshToken = await _tokens.IssueRefreshTokenAsync(usuario);
        return Ok(new
        {
            Token = accessToken,
            RefreshToken = refreshToken,
            ExpiresInMinutes = _tokens.AccessTokenMinutes,
            Usuario = usuario
        });
    }

    /// <summary>Intercambia un refresh token válido por un nuevo JWT y un nuevo refresh token (rotación).</summary>
    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _tokens.RotateRefreshAsync(dto.RefreshToken);
        if (result == null) return Unauthorized("Refresh token inválido o expirado");

        return Ok(new
        {
            Token = result.Value.AccessToken,
            RefreshToken = result.Value.RefreshToken,
            ExpiresInMinutes = _tokens.AccessTokenMinutes
        });
    }
}