using System.ComponentModel.DataAnnotations;

namespace tienda_friki.Models.DTOs;

public class CarritoCreateDTO
{
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public int UsuarioId { get; set; }
}

public class CarritoUpdateDTO
{
    public DateTime? FechaCreacion { get; set; }
    public int? UsuarioId { get; set; }
}