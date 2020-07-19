using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Unscrambles.Interfaces
{
    public interface IUnscrambleService
    {
        Task<ValidationResult> CreateUnscramble(Unscramble unscramble, IEnumerable<Exercise> exercises);
        Task<ValidationResult> UpdateUnscramble(int id, Unscramble unscramble, IEnumerable<Exercise> exercises);
        Task<ValidationResult> DeleteUnscramble(int id);
        Task<Unscramble> GetUnscramble(int id);
    }
}
