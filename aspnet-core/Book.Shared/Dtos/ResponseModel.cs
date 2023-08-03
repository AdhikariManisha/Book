namespace Book.Shared.Dtos;
public class ResponseModel<T>
{
    public ResponseModel(bool success, T data)
    {
        Success = success;
        Data = data;
    }

    public bool Success { get; set; }
    public T Data { get; set; }
}
