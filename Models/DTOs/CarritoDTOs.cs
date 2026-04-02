namespace tienda_friki.Models.DTOs;

public class CarritoCreateDTO
{
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public int UsuarioId { get; set; }
}