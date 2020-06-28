using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Unscrumbles.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Unscrumbles.Services
{
    public class UnscrumbleService : IUnscrumbleService
    {
        private readonly IUnscrumbleRepository _unscrumbleRepository;

        public UnscrumbleService(IUnscrumbleRepository unscrumbleRepository)
        {
            _unscrumbleRepository = unscrumbleRepository;
        }
        public async Task<ResultResponseBusiness> CreateUnscrumble(Unscrumble unscrumble, IEnumerable<Exercise> exercises)
        {
            if (!unscrumble.IsValid())
                return new ResultResponseBusiness(false, unscrumble.ValidationErrors);

            unscrumble.AddExercises(exercises);

            var createdUnscrumbleId = await _unscrumbleRepository.CreateUnscrumble(unscrumble);

            if (createdUnscrumbleId <= 0)
                return new ResultResponseBusiness(false, new ValidationError("There was an error while creating the activity"));

            return new ResultResponseBusiness(true);
        }

        public async Task<ResultResponseBusiness> DeleteUnscrumble(int id)
        {
            if (id <= 0) return new ResultResponseBusiness(false, new ValidationError("The activity is invalid"));

            var unscrumble = await _unscrumbleRepository.GetUnscrumble(id);

            if (unscrumble == null)
                return new ResultResponseBusiness(false, new ValidationError("The activity wasn't found"));

            var unscrumbleWasDeleted = await _unscrumbleRepository.DeleteUnscrumble(unscrumble);

            if (!unscrumbleWasDeleted)
                return new ResultResponseBusiness(false, new ValidationError("There was an error while deleting the activity"));

            return new ResultResponseBusiness(true);
        }

        public async Task<Unscrumble> GetUnscrumble(int id)
        {
            return await _unscrumbleRepository.GetUnscrumble(id);
        }

        public async Task<ResultResponseBusiness> UpdateUnscrumble(int id, Unscrumble unscrumble, IEnumerable<Exercise> exercises)
        {
            if (id <= 0) return new ResultResponseBusiness(false, new ValidationError("The activity is invalid"));

            unscrumble.Id = id;
            unscrumble.AddExercises(exercises);

            if (!unscrumble.IsValid())
                return new ResultResponseBusiness(false, unscrumble.ValidationErrors);

            var unscrumbleWasUpdated = await _unscrumbleRepository.UpdateUnscrumble(unscrumble);

            if (!unscrumbleWasUpdated)
                return new ResultResponseBusiness(false, new ValidationError("The activity wasn't found"));

            return new ResultResponseBusiness(true);
        }
    }
}
