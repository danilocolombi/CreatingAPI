using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.TicTacToes.Interfaces
{
    public interface ITicTacToeService
    {
        Task<ValidationResult> CreateAsync(TicTacToe ticTacToe, IEnumerable<TicTacToeSquare> squares);
        Task<ValidationResult> UpdateAsync(int id, TicTacToe ticTacToe, IEnumerable<TicTacToeSquare> squares);
        Task<TicTacToe> GetAsync(int id);
        Task<ValidationResult> DeleteAsync(int id);
    }
}
