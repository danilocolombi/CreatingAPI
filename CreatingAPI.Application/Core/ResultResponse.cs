using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Application.Core
{
    public class ResultResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public StatusCode StatusCode { get; set; }

        public ResultResponse(ValidationResult validationResult, Operation operation)
        {
            Success = validationResult.Success;
            Errors = validationResult.ValidationErrors.Select(s => s.Message);
            SetStatusCode(validationResult.ValidationErrors, operation);
        }

        private void SetStatusCode(IEnumerable<ValidationError> validationErrors, Operation operation)
        {
            switch (operation)
            {
                case Operation.CREATE:
                    SetCreateStatusCode(validationErrors);
                    break;
                case Operation.UPDATE:
                    SetUpdateStatusCode(validationErrors);
                    break;
                case Operation.DELETE:
                    SetDeleteStatusCode(validationErrors);
                    break;
                case Operation.GET:
                    SetGetStatusCode(validationErrors);
                    break;
                default:
                    break;
            }
        }

        private void SetCreateStatusCode(IEnumerable<ValidationError> validationErrors)
        {
            if (validationErrors.Any())
                StatusCode = StatusCode.BAD_REQUEST;
            else
                StatusCode = StatusCode.CREATED;
        }

        private void SetUpdateStatusCode(IEnumerable<ValidationError> validationErrors)
        {
            StatusCode = StatusCode.BAD_REQUEST;

            if (validationErrors.Any())
            {
                foreach (var error in validationErrors)
                {
                    if (error.Message.Contains("wasn't found"))
                    {
                        StatusCode = StatusCode.NOT_FOUND;
                        break;
                    }
                }
            }
            else
                StatusCode = StatusCode.OK;
        }

        private void SetDeleteStatusCode(IEnumerable<ValidationError> validationErrors)
        {
            if (validationErrors.Any())
                StatusCode = StatusCode.NOT_FOUND;
            else
                StatusCode = StatusCode.NO_CONTENT;
        }

        private void SetGetStatusCode(IEnumerable<ValidationError> validationErrors)
        {
            if (validationErrors.Any())
                StatusCode = StatusCode.NOT_FOUND;
            else
                StatusCode = StatusCode.OK;
        }
    }

    public class ResultResponse<T> : ResultResponse
    {
        public ResultResponse(ValidationResult validationResult, Operation operation, T value) : base(validationResult, operation)
        {
            Value = value;
        }
        public T Value { get; set; }
    }
}
