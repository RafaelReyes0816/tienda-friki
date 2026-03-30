using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tienda_friki.Models;

public class ItemCarrito
{
    [Key]
    public int Id { get; set; }

    public int Cantidad { get; set; }

    [ForeignKey(nameof(Carrito))]
    public int CarritoId { get; set; }

    public Carrito? Carrito { get; set; }

    [ForeignKey(nameof(Producto))]
    public int ProductoId { get; set; }

    public Producto? Producto { get; set; }
}
