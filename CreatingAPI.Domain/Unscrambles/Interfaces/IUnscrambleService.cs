using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Unscrambles.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Unscrambles.Interfaces
{
    public interface IUnscrambleService
    {
        Task<ValidationResult> CreateAsync(Unscramble unscramble, IEnumerable<Exercise> exercises);
        Task<ValidationResult> UpdateAsync(int id, Unscramble unscramble, IEnumerable<Exercise> exercises);
        Task<ValidationResult> DeleteAsync(int id);
        Task<Unscramble> GetAsync(int id);
        Task<IEnumerable<ShuffledExercise>> GetShuffledExercises(int idUnscramble, bool randomizeOrder);
    }
}
