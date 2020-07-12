using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.TicTacToes.Interfaces
{
    public interface ITicTacToeService
    {
        Task<ValidationResult> CreateTicTacToe(TicTacToe ticTacToe, IEnumerable<TicTacToeSquare> squares);
    }
}
