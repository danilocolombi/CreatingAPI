using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Quizzes.Interfaces
{
    public interface IQuizService
    {
        Task<ValidationResult> CreateAsync(Quiz quiz, IEnumerable<QuizQuestion> questions);
    }
}
