using System.Threading.Tasks;

namespace CreatingAPI.Domain.Unscrambles.Interfaces
{
    public interface IUnscrambleRepository
    {
        Task<int> CreateAsync(Unscramble unscramble);
        Task<bool> UpdateAsync(Unscramble unscramble);
        Task<bool> DeleteAsync(Unscramble unscramble);
        Task<Unscramble> GetAsync(int id);
    }
}
