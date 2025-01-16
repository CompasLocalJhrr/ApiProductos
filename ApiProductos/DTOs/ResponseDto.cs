namespace ApiProductos.DTOs;
public class ResponseDto<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public List<T> ListData { get; set; }
    public string ErrorMessage { get; set; }

    public ResponseDto()
    {
        Success = true;
    }

    public void SetError(string errorMessage)
    {
        ErrorMessage = errorMessage;
        Success = false;  // Cuando hay un mensaje de error, Success es false
    }
}