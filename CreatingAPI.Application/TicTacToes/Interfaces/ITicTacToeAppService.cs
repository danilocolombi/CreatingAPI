using CreatingAPI.Application.Core;
using CreatingAPI.Application.TicTacToes.ViewModels;
using System.Threading.Tasks;

namespace CreatingAPI.Application.TicTacToes.Interfaces
{
    public interface ITicTacToeAppService
    {
        Task<ResultResponse> CreateTicTacToe(TicTacToeCreationViewModel ticTacToeCreationViewModel);
        Task<ResultResponse> UpdateTicTacToe(int id, TicTacToeCreationViewModel ticTacToeCreationViewModel);
        Task<ResultResponse> DeleteTicTacToe(int id);
        Task<TicTacToeViewModel> GetTicTacToe(int id);
    }
}
