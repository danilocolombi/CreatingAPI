using System.Threading.Tasks;

namespace CreatingAPI.Domain.TicTacToes.Interfaces
{
    public interface ITicTacToeRepository
    {
        Task<int> CreateTicTacToe(TicTacToe ticTacToe);
    }
}
