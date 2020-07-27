using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.TicTacToes.Interfaces
{
    public interface ITicTacToeService
    {
        Task<ValidationResult> CreateTicTacToe(TicTacToe ticTacToe, IEnumerable<TicTacToeSquare> squares);
        Task<ValidationResult> UpdateTicTacToe(int id, TicTacToe ticTacToe, IEnumerable<TicTacToeSquare> squares);
        Task<TicTacToe> GetTicTacToe(int id);
        Task<ValidationResult> DeleteTicTacToe(int id);
    }
}
