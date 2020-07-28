using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Pickers.Interfaces
{
    public interface IPickerService
    {
        Task<ValidationResult> CreatePicker(Picker picker, IEnumerable<PickerTopic> topics);
        Task<ValidationResult> UpdatePicker(int id, Picker picker, IEnumerable<PickerTopic> topics);
        Task<ValidationResult> DeletePicker(int id);
        Task<Picker> GetPicker(int id);
    }
}
