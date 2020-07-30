using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Pickers.Interfaces
{
    public interface IPickerService
    {
        Task<ValidationResult> CreateAsync(Picker picker, IEnumerable<PickerTopic> topics);
        Task<ValidationResult> UpdateAsync(int id, Picker picker, IEnumerable<PickerTopic> topics);
        Task<ValidationResult> DeleteAsync(int id);
        Task<Picker> GetAsync(int id);
    }
}
