using Microsoft.EntityFrameworkCore;
using tienda_friki.Data;
using tienda_friki.Models;

namespace tienda_friki.Repositories;

public class RefreshTokenRepository
{
    private readonly DBContext _context;

    public RefreshTokenRepository(DBContext context) => _context = context;

    public async Task<RefreshToken?> GetActiveByHashAsync(string tokenHash)
        => await _context.RefreshTokens
            .Include(r => r.Usuario)
            .FirstOrDefaultAsync(r =>
                r.TokenHash == tokenHash
                && r.RevokedAt == null
                && r.ExpiresAt > DateTime.UtcNow);

    public async Task AddAsync(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
