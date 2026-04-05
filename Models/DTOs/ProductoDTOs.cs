using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tienda_friki.Models.DTOs;

public class ProductoCreateDTO
{
    [Required, StringLength(150)]
    public string Nombre { get; set; } = string.Empty;
    [StringLength(300)]
    public string? Imagen { get; set; }
    public decimal Precio { get; set; }
    [StringLength(100)]
    public string Franquicia { get; set; } = string.Empty;
    public bool Oferta { get; set; }
    public bool Destacado { get; set; }
    public int Stock { get; set; }
    public int CategoriaId { get; set; }
}

public class ProductoUpdateDTO
{
    [StringLength(150)]
    public string? Nombre { get; set; }
    [StringLength(300)]
    public string? Imagen { get; set; }
    public decimal? Precio { get; set; }
    [StringLength(100)]
    public string? Franquicia { get; set; }
    public bool? Oferta { get; set; }
    public bool? Destacado { get; set; }
    public int? Stock { get; set; }
    public int? CategoriaId { get; set; }
}