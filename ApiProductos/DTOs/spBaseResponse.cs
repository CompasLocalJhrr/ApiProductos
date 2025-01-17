namespace ApiProductos.DTOs;
public class spBaseResponse
{
    /// <summary>
    /// Marca estado de respuesta respecto al proceso realizado 
    /// True: si todo salió bien 
    /// False: si hubo algún error en el proceso
    /// </summary>
    public bool Success { get; set; }
    /// <summary>
    /// Mensaje de respuesta del proceso en el SP
    /// </summary>
    public string Message { get; set; }
}