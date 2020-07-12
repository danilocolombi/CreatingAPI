using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.TicTacToes.Interfaces;
using System.Threading.Tasks;

namespace CreatingAPI.Data.TicTacToes.Repository
{
    public class TicTacToeRepository : ITicTacToeRepository
    {
        public Task<int> CreateTicTacToe(TicTacToe ticTacToe)
        {
            throw new System.NotImplementedException();
        }
    }
}
