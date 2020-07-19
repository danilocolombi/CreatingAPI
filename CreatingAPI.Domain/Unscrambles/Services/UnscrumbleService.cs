using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Unscrambles.Interfaces;
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
        public async Task<ValidationResult> CreateUnscramble(Unscramble unscramble, IEnumerable<Exercise> exercises)
        {
            if (!unscramble.IsValid())
                return new ValidationResult(false, unscramble.ValidationErrors);

            unscramble.AddExercises(exercises);

            var createdUnscrumbleId = await _unscrambleRepository.CreateUnscramble(unscramble);

            if (createdUnscrumbleId <= 0)
                return new ValidationResult(false, new ValidationError("There was an error while creating the activity"));

            return new ValidationResult(true);
        }

        public async Task<ValidationResult> DeleteUnscramble(int id)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The activity is invalid"));

            var unscramble = await _unscrambleRepository.GetUnscramble(id);

            if (unscramble == null)
                return new ValidationResult(false, new ValidationError("The activity wasn't found"));

            var unscrumbleWasDeleted = await _unscrambleRepository.DeleteUnscramble(unscramble);

            if (!unscrumbleWasDeleted)
                return new ValidationResult(false, new ValidationError("There was an error while deleting the activity"));

            return new ValidationResult(true);
        }

        public async Task<Unscramble> GetUnscramble(int id)
        {
            return await _unscrambleRepository.GetUnscramble(id);
        }

        public async Task<ValidationResult> UpdateUnscramble(int id, Unscramble unscrumble, IEnumerable<Exercise> exercises)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The activity is invalid"));

            unscrumble.Id = id;
            unscrumble.AddExercises(exercises);

            if (!unscrumble.IsValid())
                return new ValidationResult(false, unscrumble.ValidationErrors);

            var unscrumbleWasUpdated = await _unscrambleRepository.UpdateUnscramble(unscrumble);

            if (!unscrumbleWasUpdated)
                return new ValidationResult(false, new ValidationError("The activity wasn't found"));

            return new ValidationResult(true);
        }
    }
}
