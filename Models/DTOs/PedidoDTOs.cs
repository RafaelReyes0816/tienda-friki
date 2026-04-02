using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tienda_friki.Models.DTOs;

public class PedidoCreateDTO
{
    [Required, StringLength(50)]
    public string Codigo { get; set; } = string.Empty;

    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    [Required, StringLength(30)]
    public string Estado { get; set; } = "Pendiente";

    [Column(TypeName = "numeric(10,2)")]
    public decimal Total { get; set; }

    public int UsuarioId { get; set; }
}