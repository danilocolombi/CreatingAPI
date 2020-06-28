using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Domain.Core
{
    public class Entity
    {
        public int Id { get; set; }
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public bool IsValid() => !ValidationErrors.Any();

    }
}
