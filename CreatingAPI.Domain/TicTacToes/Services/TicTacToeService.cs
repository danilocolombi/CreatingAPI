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

        public async Task<ValidationResult> UpdateTicTacToe(int id, TicTacToe ticTacToe, IEnumerable<TicTacToeSquare> squares)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The activity is invalid"));

            ticTacToe.SetId(id);

            if (!ticTacToe.IsValid())
                return new ValidationResult(false, ticTacToe.ValidationErrors);

            if (!ticTacToe.CreateSquares(squares))
                return new ValidationResult(false, ticTacToe.ValidationErrors);

            var ticTacToeWasUpdated = await _ticTacToeRepository.UpdateTicTacToe(ticTacToe);

            if (!ticTacToeWasUpdated)
                return new ValidationResult(false, new ValidationError("The activity wasn't found"));

            return new ValidationResult(true);
        }

        public async Task<TicTacToe> GetTicTacToe(int id)
        {
            return await _ticTacToeRepository.GetTicTacToe(id);
        }

        public async Task<ValidationResult> DeleteTicTacToe(int id)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The activity is invalid"));

            var ticTacToe = await _ticTacToeRepository.GetTicTacToe(id);

            if (ticTacToe == null)
                return new ValidationResult(false, new ValidationError("The activity wasn't found"));

            var ticTacToeWasDeleted = await _ticTacToeRepository.DeleteTicTacToe(ticTacToe);

            if (!ticTacToeWasDeleted)
                return new ValidationResult(false, new ValidationError("There was an error while deleting the activity"));

            return new ValidationResult(true);
        }
    }
}
