using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Core;
using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Domain.Quizzes
{
    public class Quiz : Activity
    {
        public ICollection<QuizQuestion> Questions { get; private set; }

        private const int MAX_NUMBER_OF_QUESTIONS = 50;
        public Quiz(string title, int userId, bool isPublic) : base(title, userId, isPublic)
        {

        }

        public bool AddQuestions(IEnumerable<QuizQuestion> questions)
        {
            if (questions.Count() > MAX_NUMBER_OF_QUESTIONS)
                ValidationErrors.Add(new ValidationError($"A quiz can't have more than {MAX_NUMBER_OF_QUESTIONS} questions"));

            Questions = new List<QuizQuestion>(questions.Count());

            var cont = 1;

            foreach (var question in questions)
            {
                if (!question.IsValid())
                {
                    ValidationErrors.Add(new ValidationError($"Question {cont} is invalid: Error {question.ValidationErrors.FirstOrDefault().Message}"));
                    return false;
                }

                if (Id > 0)
                    question.SetQuizId(Id);

                Questions.Add(question);
                cont++;
            }

            return true;
        }

        public bool RemoveQuestion(int questionId)
        {
            var question = Questions.FirstOrDefault(q => q.Id == questionId);

            if (question == null)
            {
                ValidationErrors.Add(new ValidationError($"The question wasn't found"));
                return false;
            }

            return Questions.Remove(question);
        }

        public bool AddQuestion(QuizQuestion question)
        {
            if (!question.IsValid())
            {
                ValidationErrors.Add(new ValidationError($"The question is invalid: Error {question.ValidationErrors.FirstOrDefault().Message}"));
                return false;
            }

            if (Questions.Count() + 1 > MAX_NUMBER_OF_QUESTIONS)
            {
                ValidationErrors.Add(new ValidationError($"A quiz can't have more than {MAX_NUMBER_OF_QUESTIONS} questions"));
                return false;
            }

            Questions.Add(question);
            return true;
        }
    }
}
