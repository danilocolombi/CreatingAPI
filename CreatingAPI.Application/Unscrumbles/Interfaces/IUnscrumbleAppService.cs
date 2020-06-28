using CreatingAPI.Application.Core;
using CreatingAPI.Application.Unscrumbles.ViewModels;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Unscrumbles.Interfaces
{
    public interface IUnscrumbleAppService
    {
        Task<ResultResponse> CreateUnscrumble(UnscrumbleCreationViewModel unscrumbleCreationViewModel);
        Task<ResultResponse> UpdateUnscrumble(int id, UnscrumbleCreationViewModel unscrumbleCreationViewModel);
        Task<ResultResponse> DeleteUnscrumble(int idUnscrumble);
        Task<UnscrumbleViewModel> GetUnscrumble(int idUnscrumble);
    }
}
