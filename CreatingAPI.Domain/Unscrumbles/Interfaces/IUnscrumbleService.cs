using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Unscrumbles.Interfaces
{
    public interface IUnscrumbleService
    {
        Task<ResultResponseBusiness> CreateUnscrumble(Unscrumble unscrumble, IEnumerable<Exercise> exercises);
        Task<ResultResponseBusiness> UpdateUnscrumble(int id, Unscrumble unscrumble, IEnumerable<Exercise> exercises);
        Task<ResultResponseBusiness> DeleteUnscrumble(int id);
        Task<Unscrumble> GetUnscrumble(int id);
    }
}
