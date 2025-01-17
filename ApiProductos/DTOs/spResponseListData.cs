namespace ApiProductos.DTOs;
public class spResponseListData<T> : spBaseResponse
{ 
    /// <summary>
    /// Lista de datos que almacena la respuesya del SP cuando es la respuesta a una consulta de varias Filas
    /// </summary>
    public List<T> ListData { get; set; }
}