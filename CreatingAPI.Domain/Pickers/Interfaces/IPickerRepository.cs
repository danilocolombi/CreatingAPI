using System.Threading.Tasks;

namespace CreatingAPI.Domain.Pickers.Interfaces
{
    public interface IPickerRepository
    {
        Task<int> CreatePicker(Picker picker);
        Task<bool> UpdatePicker(Picker picker);
        Task<Picker> GetPicker(int id);
        Task<bool> DeletePicker(Picker picker);
    }
}
