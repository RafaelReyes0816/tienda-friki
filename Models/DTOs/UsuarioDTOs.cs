using System.ComponentModel.DataAnnotations;

namespace tienda_friki.Models.DTOs;

public class UsuarioCreateDTO
{
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
}

public class UsuarioLoginDTO
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Contrasena { get; set; } = string.Empty;
}

public class UsuarioUpdateDTO
{
    [StringLength(100)]
    public string? Nombre { get; set; }
    [EmailAddress, StringLength(150)]
    public string? Email { get; set; }
    [StringLength(30)]
    public string? Telefono { get; set; }
    [StringLength(200)]
    public string? Contrasena { get; set; }
    [StringLength(30)]
    public string? Rol { get; set; }
}