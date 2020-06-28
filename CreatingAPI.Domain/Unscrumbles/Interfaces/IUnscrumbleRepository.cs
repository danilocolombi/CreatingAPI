using System.Threading.Tasks;

namespace CreatingAPI.Domain.Unscrumbles.Interfaces
{
    public interface IUnscrumbleRepository
    {
        Task<int> CreateUnscrumble(Unscrumble unscrumble);
        Task<bool> UpdateUnscrumble(Unscrumble unscrumble);
        Task<bool> DeleteUnscrumble(Unscrumble unscrumble);
        Task<Unscrumble> GetUnscrumble(int id);
    }
}
