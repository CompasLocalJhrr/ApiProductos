namespace ApiProductos.DTOs;
public class spResponseData<T> : spBaseResponse
{ 
    /// <summary>
    /// Data que almacena la respuesya del SP cuando es una sola fila
    /// </summary>
    public T Data { get; set; }
}