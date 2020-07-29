using System.Collections.Generic;

namespace CreatingAPI.Domain.Core
{
    public class ValidationResult
    {
        public bool Success { get; set; }
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public ValidationResult() { }
        public ValidationResult(bool success, IEnumerable<ValidationError> validationErrors)
        {
            Success = success;
            ValidationErrors.AddRange(validationErrors);
        }

        public ValidationResult(bool success)
        {
            Success = success;
        }

        public ValidationResult(bool success, ValidationError ValidationError)
        {
            Success = success;
            ValidationErrors.Add(ValidationError);
        }
    }
}
