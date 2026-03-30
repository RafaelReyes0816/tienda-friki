using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tienda_friki.Models;

public class Producto
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Imagen { get; set; }

    [Column(TypeName = "numeric(10,2)")]
    public decimal Precio { get; set; }

    [StringLength(100)]
    public string Franquicia { get; set; } = string.Empty;

    public bool Oferta { get; set; }

    public bool Destacado { get; set; }

    public int Stock { get; set; }

    [ForeignKey(nameof(Categoria))]
    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }

    public ICollection<ItemCarrito> ItemsCarrito { get; set; } = new List<ItemCarrito>();

    public ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
}
