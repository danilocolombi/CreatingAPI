using System.Collections.Generic;

namespace CreatingAPI.Domain.Core
{
    public class ResultResponseBusiness
    {
        public bool Success { get; set; }
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public ResultResponseBusiness(bool success, IEnumerable<ValidationError> validationErrors)
        {
            Success = success;
            ValidationErrors.AddRange(validationErrors);
        }

        public ResultResponseBusiness(bool success)
        {
            Success = success;
        }

        public ResultResponseBusiness(bool success, ValidationError ValidationError)
        {
            Success = success;
            ValidationErrors.Add(ValidationError);
        }
    }
}
