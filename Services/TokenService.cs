using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using tienda_friki.Models;
using tienda_friki.Repositories;

namespace tienda_friki.Services;

public class TokenService
{
    private readonly IConfiguration _config;
    private readonly RefreshTokenRepository _refreshTokens;

    public TokenService(IConfiguration config, RefreshTokenRepository refreshTokens)
    {
        _config = config;
        _refreshTokens = refreshTokens;
    }

    public int AccessTokenMinutes =>
        _config.GetValue("Jwt:AccessTokenMinutes", 15);

    private int RefreshTokenDays =>
        _config.GetValue("Jwt:RefreshTokenDays", 7);

    public string CreateAccessToken(Usuario usuario)
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

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(AccessTokenMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>Genera un refresh token opaco y lo guarda hasheado. Devuelve el valor en claro para el cliente.</summary>
    public async Task<string> IssueRefreshTokenAsync(Usuario usuario)
    {
        var plain = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var entity = new RefreshToken
        {
            UsuarioId = usuario.Id,
            TokenHash = HashToken(plain),
            ExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenDays),
            CreatedAt = DateTime.UtcNow
        };
        await _refreshTokens.AddAsync(entity);
        return plain;
    }

    public async Task<(string AccessToken, string RefreshToken)?> RotateRefreshAsync(string plainRefreshToken)
    {
        var hash = HashToken(plainRefreshToken);
        var stored = await _refreshTokens.GetActiveByHashAsync(hash);
        if (stored == null) return null;

        stored.RevokedAt = DateTime.UtcNow;
        await _refreshTokens.SaveAsync();

        var usuario = stored.Usuario;
        var access = CreateAccessToken(usuario);
        var newRefresh = await IssueRefreshTokenAsync(usuario);
        return (access, newRefresh);
    }

    private static string HashToken(string plain)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(plain));
        return Convert.ToBase64String(bytes);
    }
}
