using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tienda_friki.Models;

public class Pedido
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Codigo { get; set; } = string.Empty;

    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    [Required, StringLength(30)]
    public string Estado { get; set; } = "Pendiente";

    [Column(TypeName = "numeric(10,2)")]
    public decimal Total { get; set; }

    [ForeignKey(nameof(Usuario))]
    public int UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }

    public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
}
