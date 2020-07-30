using System.Collections.Generic;

namespace CreatingAPI.Application.Quizzes.ViewModels
{
    public class QuizQuestionCreationViewModel
    {
        public string Description { get; set; }
        public IEnumerable<AlternativeViewModel> Alternatives { get; set; }
    }
}
