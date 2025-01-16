namespace ApiProductos.DTOs;

public partial class ResposeProductoDto
{
    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? Stock { get; set; }
}
