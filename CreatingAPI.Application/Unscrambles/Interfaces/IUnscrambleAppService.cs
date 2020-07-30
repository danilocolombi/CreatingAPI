using CreatingAPI.Application.Core;
using CreatingAPI.Application.Unscrambles.ViewModels;
using CreatingAPI.Domain.Unscrambles.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Unscrambles.Interfaces
{
    public interface IUnscrambleAppService
    {
        Task<ResultResponse> CreateAsync(UnscrambleCreationViewModel unscrambleCreationViewModel);
        Task<ResultResponse> UpdateAsync(int id, UnscrambleCreationViewModel unscrambleCreationViewModel);
        Task<ResultResponse> DeleteAsync(int idUnscramble);
        Task<UnscrambleViewModel> GetAsync(int idUnscramble);
        Task<IEnumerable<ShuffledExercise>> GetShuffledExercises(int idUnscramble, bool randomizeOrder);

    }
}
