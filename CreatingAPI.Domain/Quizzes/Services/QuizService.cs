using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Quizzes.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Quizzes.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<ValidationResult> CreateAsync(Quiz quiz, IEnumerable<QuizQuestion> questions)
        {
            if (!quiz.IsValid())
                return new ValidationResult(false, quiz.ValidationErrors);

            if (!quiz.AddQuestions(questions))
                return new ValidationResult(false, quiz.ValidationErrors);

            var createdQuizId = await _quizRepository.CreateAsync(quiz);

            if (createdQuizId <= 0)
                return new ValidationResult(false, new ValidationError("There was an error while creating the activity"));

            return new ValidationResult(true);
        }

        public async Task<ValidationResult> DeleteAsync(int id)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The activity is invalid"));

            var quiz = await _quizRepository.GetAsync(id);

            if (quiz == null)
                return new ValidationResult(false, new ValidationError("The activity wasn't found"));

            var quizWasDeleted = await _quizRepository.DeleteAsync(quiz);

            if (!quizWasDeleted)
                return new ValidationResult(false, new ValidationError("There was an error while deleting the activity"));

            return new ValidationResult(true);
        }

        public async Task<Quiz> GetAsync(int id)
        {
            return await _quizRepository.GetAsync(id);
        }

        public async Task<ValidationResult> UpdateAsync(int id, Quiz quiz, IEnumerable<QuizQuestion> questions)
        {
            if (id <= 0) return new ValidationResult(false, new ValidationError("The activity is invalid"));

            quiz.SetId(id);

            if (!quiz.IsValid())
                return new ValidationResult(false, quiz.ValidationErrors);

            if (!quiz.AddQuestions(questions))
                return new ValidationResult(false, quiz.ValidationErrors);

            var quizWasUpdated = await _quizRepository.UpdateAsync(quiz);

            if (!quizWasUpdated)
                return new ValidationResult(false, new ValidationError("The activity wasn't found"));

            return new ValidationResult(true);
        }
    }
}
