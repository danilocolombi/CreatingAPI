using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Pickers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Pickers.Services
{
    public class PickerService : IPickerService
    {
        private readonly IPickerRepository _pickerRepository;

        public PickerService(IPickerRepository pickerRepository)
        {
            _pickerRepository = pickerRepository;
        }
        public async Task<ValidationResult> CreateAsync(Picker picker, IEnumerable<PickerTopic> topics)
        {
            if (!picker.IsValid())
                return new ValidationResult(false, picker.ValidationErrors);

            if (!picker.AddTopics(topics))
                return new ValidationResult(false, picker.ValidationErrors);

            var createdPickerId = await _pickerRepository.CreateAsync(picker);

            if (createdPickerId <= 0)
                return new ValidationResult(false, new ValidationError("There was an error while creating the activity"));

            return new ValidationResult(true);
        }

        public async Task<ValidationResult> UpdateAsync(int id, Picker picker, IEnumerable<PickerTopic> topics)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The activity is invalid"));

            picker.SetId(id);

            if (!picker.IsValid())
                return new ValidationResult(false, picker.ValidationErrors);

            if (!picker.AddTopics(topics))
                return new ValidationResult(false, picker.ValidationErrors);

            var pickerWasUpdated = await _pickerRepository.UpdateAsync(picker);

            if (!pickerWasUpdated)
                return new ValidationResult(false, new ValidationError("The activity wasn't found"));

            return new ValidationResult(true);
        }

        public async Task<ValidationResult> DeleteAsync(int id)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The activity is invalid"));

            var picker = await _pickerRepository.GetAsync(id);

            if (picker == null)
                return new ValidationResult(false, new ValidationError("The activity wasn't found"));

            var pickerWasDeleted = await _pickerRepository.DeleteAsync(picker);

            if (!pickerWasDeleted)
                return new ValidationResult(false, new ValidationError("There was an error while deleting the activity"));

            return new ValidationResult(true);
        }

        public async Task<Picker> GetAsync(int id)
        {
            return await _pickerRepository.GetAsync(id);
        }
    }
}
