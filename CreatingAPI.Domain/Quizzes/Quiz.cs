using CreatingAPI.Domain.Activities;
using System.Collections.Generic;

namespace CreatingAPI.Domain.Quizzes
{
    public class Quiz : Activity
    {
        public ICollection<QuizQuestion> Questions { get; set; }

        public Quiz(string title, int userId, bool isPublic) : base(title, userId, isPublic)
        {

        }
    }
}
