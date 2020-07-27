using System.Threading.Tasks;

namespace CreatingAPI.Domain.TicTacToes.Interfaces
{
    public interface ITicTacToeRepository
    {
        Task<int> CreateTicTacToe(TicTacToe ticTacToe);
        Task<bool> UpdateTicTacToe(TicTacToe ticTacToe);
        Task<TicTacToe> GetTicTacToe(int id);
        Task<bool> DeleteTicTacToe(TicTacToe ticTacToe);
    }
}
