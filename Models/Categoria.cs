using System.ComponentModel.DataAnnotations;

namespace tienda_friki.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
