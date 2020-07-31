using CreatingAPI.Application.Core;
using CreatingAPI.Application.Quizzes.ViewModels;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Quizzes.Interfaces
{
    public interface IQuizAppService
    {
        Task<ResultResponse> CreateAsync(QuizCreationViewModel quizCreationViewModel);
        Task<ResultResponse> UpdateAsync(int id, QuizCreationViewModel quizCreationViewModel);
        Task<ResultResponse> DeleteAsync(int id);
        Task<QuizViewModel> GetAsync(int id);
    }
}
