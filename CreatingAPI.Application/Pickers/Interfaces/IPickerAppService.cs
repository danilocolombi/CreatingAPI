using CreatingAPI.Application.Core;
using CreatingAPI.Application.Pickers.ViewModels;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Pickers.Interfaces
{
    public interface IPickerAppService
    {
        Task<ResultResponse> CreateAsync(PickerCreationViewModel pickerCreationViewModel);
        Task<ResultResponse> UpdateAsync(int id, PickerCreationViewModel pickerCreationViewModel);
        Task<ResultResponse> DeleteAsync(int id);
        Task<PickerViewModel> GetAsync(int id);
    }
}
