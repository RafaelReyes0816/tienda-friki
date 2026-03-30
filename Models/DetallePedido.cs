using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tienda_friki.Models;

public class DetallePedido
{
    [Key]
    public int Id { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "numeric(10,2)")]
    public decimal PrecioUnitario { get; set; }

    [Column(TypeName = "numeric(10,2)")]
    public decimal Subtotal { get; set; }

    [ForeignKey(nameof(Pedido))]
    public int PedidoId { get; set; }

    public Pedido? Pedido { get; set; }

    [ForeignKey(nameof(Producto))]
    public int ProductoId { get; set; }

    public Producto? Producto { get; set; }
}
