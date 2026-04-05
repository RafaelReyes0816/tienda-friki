using System.ComponentModel.DataAnnotations.Schema;

namespace tienda_friki.Models.DTOs;

public class DetallePedidoCreateDTO
{
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
    public int PedidoId { get; set; }
    public int ProductoId { get; set; }
}

public class DetallePedidoUpdateDTO
{
    public int? Cantidad { get; set; }
    public decimal? PrecioUnitario { get; set; }
    public decimal? Subtotal { get; set; }
    public int? PedidoId { get; set; }
    public int? ProductoId { get; set; }
}