using CreatingAPI.Application.Core;
using CreatingAPI.Application.Unscrambles.ViewModels;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Unscrambles.Interfaces
{
    public interface IUnscrambleAppService
    {
        Task<ResultResponse> CreateUnscramble(UnscrambleCreationViewModel unscrambleCreationViewModel);
        Task<ResultResponse> UpdateUnscramble(int id, UnscrambleCreationViewModel unscrambleCreationViewModel);
        Task<ResultResponse> DeleteUnscramble(int idUnscramble);
        Task<UnscrambleViewModel> GetUnscramble(int idUnscramble);
    }
}
