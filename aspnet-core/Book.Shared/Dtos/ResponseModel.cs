using System.ComponentModel.DataAnnotations;

namespace Book.Shared.Dtos;
public class ResponseModel<T>
{
    public ResponseModel()
    {
    }

    public ResponseModel(bool success)
    {
        Success = success;
    }

    public ResponseModel(bool success, T data)
    {
        Success = success;
        Data = data;
    }

    public ResponseModel(bool success, T data, List<ValidationResult> errors)
    {
        Success = success;
        Data = data;
        Errors = errors;
    }

    public bool Success { get; set; }

    public T Data { get; set; }
    public List<ValidationResult> Errors { get; set; }
}
