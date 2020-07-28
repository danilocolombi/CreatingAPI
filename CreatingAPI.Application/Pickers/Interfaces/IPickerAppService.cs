using CreatingAPI.Application.Core;
using CreatingAPI.Application.Pickers.ViewModels;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Pickers.Interfaces
{
    public interface IPickerAppService
    {
        Task<ResultResponse> CreatePicker(PickerCreationViewModel pickerCreationViewModel);
        Task<ResultResponse> UpdatePicker(int id, PickerCreationViewModel pickerCreationViewModel);
        Task<ResultResponse> DeletePicker(int id);
        Task<PickerViewModel> GetPicker(int id);
    }
}
