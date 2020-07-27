using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Unscrambles.ValueObjects;
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
        Task<IEnumerable<ShuffledExercise>> GetShuffledExercises(int idUnscramble, bool randomizeOrder);
    }
}
