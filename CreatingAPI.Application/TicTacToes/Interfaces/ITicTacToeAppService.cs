using CreatingAPI.Application.Core;
using CreatingAPI.Application.TicTacToes.ViewModels;
using System.Threading.Tasks;

namespace CreatingAPI.Application.TicTacToes.Interfaces
{
    public interface ITicTacToeAppService
    {
        Task<ResultResponse> CreateAsync(TicTacToeCreationViewModel ticTacToeCreationViewModel);
        Task<ResultResponse> UpdateAsync(int id, TicTacToeCreationViewModel ticTacToeCreationViewModel);
        Task<ResultResponse> DeleteAsync(int id);
        Task<TicTacToeViewModel> GetAsync(int id);
    }
}
