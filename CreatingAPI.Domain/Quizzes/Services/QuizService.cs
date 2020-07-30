using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Quizzes.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Quizzes.Services
{
    public class QuizService : IQuizService
    {
        public Task<ValidationResult> CreateAsync(Quiz quiz, IEnumerable<QuizQuestion> questions)
        {
            throw new System.NotImplementedException();
        }
    }
}
