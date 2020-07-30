using CreatingAPI.Application.Activities.ViewModels;
using System.Collections.Generic;

namespace CreatingAPI.Application.Quizzes.ViewModels
{
    public class QuizCreationViewModel : ActivityCreationViewModel
    {
        public IEnumerable<QuizQuestionCreationViewModel> Questions { get; set; }
    }
}
