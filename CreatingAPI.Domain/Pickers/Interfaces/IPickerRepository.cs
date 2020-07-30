using System.Threading.Tasks;

namespace CreatingAPI.Domain.Pickers.Interfaces
{
    public interface IPickerRepository
    {
        Task<int> CreateAsync(Picker picker);
        Task<bool> UpdateAsync(Picker picker);
        Task<Picker> GetAsync(int id);
        Task<bool> DeleteAsync(Picker picker);
    }
}
