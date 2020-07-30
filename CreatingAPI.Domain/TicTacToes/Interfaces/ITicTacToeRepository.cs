using System.Threading.Tasks;

namespace CreatingAPI.Domain.TicTacToes.Interfaces
{
    public interface ITicTacToeRepository
    {
        Task<int> CreateAsync(TicTacToe ticTacToe);
        Task<bool> UpdateAsync(TicTacToe ticTacToe);
        Task<TicTacToe> GetAsync(int id);
        Task<bool> DeleteAsync(TicTacToe ticTacToe);
    }
}
