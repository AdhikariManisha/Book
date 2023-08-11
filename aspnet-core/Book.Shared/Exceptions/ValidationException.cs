using System.ComponentModel.DataAnnotations;

namespace Book.Shared.Exceptions;
public class ValidationException : Exception 
{
    public ValidationException() { 
    }
    public ValidationException(string message, List<ValidationResult> validationErrors) : base(message) {
        ValidationErrors = validationErrors;
    }

    public List<ValidationResult> ValidationErrors { get; }
}
