using System.ComponentModel.DataAnnotations;

namespace tienda_friki.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Telefono { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Contrasena { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Rol { get; set; } = "Cliente";

    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public Carrito? Carrito { get; set; }
}
