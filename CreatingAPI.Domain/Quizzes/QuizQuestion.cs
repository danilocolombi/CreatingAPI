using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Quizzes.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace CreatingAPI.Domain.Quizzes
{
    public class QuizQuestion : Entity
    {
        public string Description { get; set; }
        public Alternative AlternativeA { get; private set; }
        public Alternative AlternativeB { get; set; }
        public Alternative? AlternativeC { get; private set; }
        public Alternative? AlternativeD { get; private set; }
        public int QuizId { get; private set; }
        public virtual Quiz Quiz { get; }

        private const int MIN_NUMBER_OF_ALTERNATIVES = 2;
        private const int MAX_NUMBER_OF_ALTERNATIVES = 4;

        public QuizQuestion() { }
        public QuizQuestion(string description, IEnumerable<Alternative> alternatives)
        {
            Description = description;
            SetAlternatives(alternatives);
        }

        public void SetQuizId(int quizId)
        {
            QuizId = quizId;
        }

        public bool SetAlternatives(IEnumerable<Alternative> alternatives)
        {
            if (alternatives.Count() < MIN_NUMBER_OF_ALTERNATIVES)
                ValidationErrors.Add(new ValidationError($"You need at least {MIN_NUMBER_OF_ALTERNATIVES} to create a question"));

            if (alternatives.Count() > MAX_NUMBER_OF_ALTERNATIVES)
                ValidationErrors.Add(new ValidationError($"You can't have more than {MIN_NUMBER_OF_ALTERNATIVES} in a question"));

            bool allAlternativesAreWrong = true;
            int counter = 1;

            foreach (var alternative in alternatives)
            {
                if (counter == 1)
                    AlternativeA = alternative;

                else if (counter == 2)
                    AlternativeB = alternative;

                else if (counter == 3)
                    AlternativeC = alternative;

                else
                    AlternativeD = alternative;

                counter++;

                if (alternative.IsCorrect)
                    allAlternativesAreWrong = false;
            }

            if (allAlternativesAreWrong)
                ValidationErrors.Add(new ValidationError("There are no right alternatives"));

            return true;
        }
    }
}
