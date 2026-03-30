using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tienda_friki.Models;

public class Carrito
{
    [Key]
    public int Id { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(Usuario))]
    public int UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }

    public ICollection<ItemCarrito> Items { get; set; } = new List<ItemCarrito>();
}
