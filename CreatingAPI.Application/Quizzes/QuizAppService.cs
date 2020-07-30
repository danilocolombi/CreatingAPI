using AutoMapper;
using CreatingAPI.Application.Core;
using CreatingAPI.Application.Quizzes.Interfaces;
using CreatingAPI.Application.Quizzes.ViewModels;
using CreatingAPI.Domain.Quizzes;
using CreatingAPI.Domain.Quizzes.Interfaces;
using CreatingAPI.Domain.Quizzes.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Quizzes
{
    public class QuizAppService : IQuizAppService
    {
        private readonly IQuizService _quizService;
        private readonly IMapper _mapper;

        public QuizAppService(IQuizService quizService, IMapper mapper)
        {
            _quizService = quizService;
            _mapper = mapper;
        }

        public async Task<ResultResponse> CreateAsync(QuizCreationViewModel quizCreationViewModel)
        {
            var quiz = _mapper.Map<Quiz>(quizCreationViewModel);
            var questions = new List<QuizQuestion>(quizCreationViewModel.Questions.Count());

            foreach (var questionViewModel in quizCreationViewModel.Questions)
            {
                var alternatives = new List<Alternative>(4);

                foreach (var alternativeViewModel in questionViewModel.Alternatives)
                    alternatives.Add(Alternative.Parse(alternativeViewModel.description, alternativeViewModel.isCorrect));

                questions.Add(new QuizQuestion(questionViewModel.Description, alternatives));
            }

            var businessResult = await _quizService.CreateAsync(quiz, questions);

            return new ResultResponse(businessResult, Operation.CREATE);
        }
    }
}
