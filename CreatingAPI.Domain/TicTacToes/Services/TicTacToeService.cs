using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.TicTacToes.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.TicTacToes.Services
{
    public class TicTacToeService : ITicTacToeService
    {
        private readonly ITicTacToeRepository _ticTacToeRepository;
        public TicTacToeService(ITicTacToeRepository ticTacToeRepository)
        {
            _ticTacToeRepository = ticTacToeRepository;
        }
        public async Task<ValidationResult> CreateTicTacToe(TicTacToe ticTacToe, IEnumerable<TicTacToeSquare> squares)
        {
            if (!ticTacToe.IsValid())
                return new ValidationResult(false, ticTacToe.ValidationErrors);

            if (!ticTacToe.CreateSquares(squares))
                return new ValidationResult(false, ticTacToe.ValidationErrors);

            var createdTicTacToeId = await _ticTacToeRepository.CreateTicTacToe(ticTacToe);

            if (createdTicTacToeId <= 0)
                return new ValidationResult(false, new ValidationError("There was an error while creating the activity"));

            return new ValidationResult(true);
        }
    }
}
