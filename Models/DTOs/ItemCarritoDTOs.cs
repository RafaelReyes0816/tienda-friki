namespace tienda_friki.Models.DTOs;

public class ItemCarritoCreateDTO
{
    public int Cantidad { get; set; }
    public int CarritoId { get; set; }
    public int ProductoId { get; set; }
}

public class ItemCarritoUpdateDTO
{
    public int? Cantidad { get; set; }
    public int? CarritoId { get; set; }
    public int? ProductoId { get; set; }
}