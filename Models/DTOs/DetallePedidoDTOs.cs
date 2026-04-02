using System.ComponentModel.DataAnnotations.Schema;

namespace tienda_friki.Models.DTOs;

public class DetallePedidoCreateDTO
{
    public int Cantidad { get; set; }

    [Column(TypeName = "numeric(10,2)")]
    public decimal PrecioUnitario { get; set; }

    [Column(TypeName = "numeric(10,2)")]
    public decimal Subtotal { get; set; }

    public int PedidoId { get; set; }
    public int ProductoId { get; set; }
}