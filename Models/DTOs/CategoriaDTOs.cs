using System.ComponentModel.DataAnnotations;

namespace tienda_friki.Models.DTOs;

public class CategoriaCreateDTO
{
    [Required, StringLength(120)]
    public string Nombre { get; set; } = string.Empty;
}