using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Domain.Core
{
    public class Entity
    {
        public int Id { get; private set; }

        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public bool IsValid() => !ValidationErrors.Any();

        public bool SetId(int id)
        {
            if (id <= 0)
            {
                ValidationErrors.Add(new ValidationError("The id is invalid"));
                return false;
            }

            Id = id;
            return true;
        }
    }
}
