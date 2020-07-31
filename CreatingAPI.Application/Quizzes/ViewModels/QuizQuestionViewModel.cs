namespace CreatingAPI.Application.Quizzes.ViewModels
{
    public class QuizQuestionViewModel
    {
        public string Description { get; set; }
        public AlternativeViewModel AlternativeA { get; set; }
        public AlternativeViewModel AlternativeB { get; set; }
        public AlternativeViewModel AlternativeC { get; set; }
        public AlternativeViewModel AlternativeD { get; set; }
    }
}
