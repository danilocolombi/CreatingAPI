using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Unscrumbles.Interfaces
{
    public interface IUnscrumbleService
    {
        Task<ValidationResult> CreateUnscrumble(Unscrumble unscrumble, IEnumerable<Exercise> exercises);
        Task<ValidationResult> UpdateUnscrumble(int id, Unscrumble unscrumble, IEnumerable<Exercise> exercises);
        Task<ValidationResult> DeleteUnscrumble(int id);
        Task<Unscrumble> GetUnscrumble(int id);
    }
}
