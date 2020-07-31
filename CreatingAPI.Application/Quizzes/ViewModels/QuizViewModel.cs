using CreatingAPI.Application.Activities.ViewModels;
using System.Collections.Generic;

namespace CreatingAPI.Application.Quizzes.ViewModels
{
    public class QuizViewModel : ActivityViewModel
    {
        public ICollection<QuizQuestionViewModel> Questions { get; set; }
    }
}
