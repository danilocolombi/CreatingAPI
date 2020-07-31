using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Quizzes.Interfaces
{
    public interface IQuizService
    {
        Task<ValidationResult> CreateAsync(Quiz quiz, IEnumerable<QuizQuestion> questions);
        Task<ValidationResult> UpdateAsync(int id, Quiz quiz, IEnumerable<QuizQuestion> questions);
        Task<Quiz> GetAsync(int id);
        Task<ValidationResult> DeleteAsync(int id);
    }
}
