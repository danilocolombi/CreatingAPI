using System.Threading.Tasks;

namespace CreatingAPI.Domain.Unscrambles.Interfaces
{
    public interface IUnscrambleRepository
    {
        Task<int> CreateUnscramble(Unscramble unscramble);
        Task<bool> UpdateUnscramble(Unscramble unscramble);
        Task<bool> DeleteUnscramble(Unscramble unscramble);
        Task<Unscramble> GetUnscramble(int id);
    }
}
