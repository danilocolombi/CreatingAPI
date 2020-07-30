using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Unscrambles.Interfaces;
using CreatingAPI.Domain.Unscrambles.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Unscrambles.Services
{
    public class UnscrumbleService : IUnscrambleService
    {
        private readonly IUnscrambleRepository _unscrambleRepository;

        public UnscrumbleService(IUnscrambleRepository unscrambleRepository)
        {
            _unscrambleRepository = unscrambleRepository;
        }
        public async Task<ValidationResult> CreateAsync(Unscramble unscramble, IEnumerable<Exercise> exercises)
        {
            if (!unscramble.IsValid())
                return new ValidationResult(false, unscramble.ValidationErrors);

            if (!unscramble.AddExercises(exercises))
                return new ValidationResult(false, unscramble.ValidationErrors);

            var createdUnscrumbleId = await _unscrambleRepository.CreateAsync(unscramble);

            if (createdUnscrumbleId <= 0)
                return new ValidationResult(false, new ValidationError("There was an error while creating the activity"));

            return new ValidationResult(true);
        }

        public async Task<ValidationResult> DeleteAsync(int id)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The activity is invalid"));

            var unscramble = await _unscrambleRepository.GetAsync(id);

            if (unscramble == null)
                return new ValidationResult(false, new ValidationError("The activity wasn't found"));

            var unscrambleWasDeleted = await _unscrambleRepository.DeleteAsync(unscramble);

            if (!unscrambleWasDeleted)
                return new ValidationResult(false, new ValidationError("There was an error while deleting the activity"));

            return new ValidationResult(true);
        }

        public async Task<Unscramble> GetAsync(int id)
        {
            return await _unscrambleRepository.GetAsync(id);
        }

        public async Task<ValidationResult> UpdateAsync(int id, Unscramble unscramble, IEnumerable<Exercise> exercises)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The activity is invalid"));

            unscramble.SetId(id);

            if (!unscramble.AddExercises(exercises))
                return new ValidationResult(false, unscramble.ValidationErrors);

            if (!unscramble.IsValid())
                return new ValidationResult(false, unscramble.ValidationErrors);

            var unscrambleWasUpdated = await _unscrambleRepository.UpdateAsync(unscramble);

            if (!unscrambleWasUpdated)
                return new ValidationResult(false, new ValidationError("The activity wasn't found"));

            return new ValidationResult(true);
        }

        public async Task<IEnumerable<ShuffledExercise>> GetShuffledExercises(int idUnscramble, bool randomizeOrder)
        {
            var unscramble = await _unscrambleRepository.GetAsync(idUnscramble);

            if (unscramble == null)
                return null;

            return unscramble.GetShuffledExercises(randomizeOrder);
        }
    }
}
