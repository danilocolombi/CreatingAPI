using AutoMapper;
using CreatingAPI.Application.Activities.ViewModels;
using CreatingAPI.Application.Bookmarks.ViewModels;
using CreatingAPI.Application.Games.ViewModels;
using CreatingAPI.Application.Pickers.ViewModels;
using CreatingAPI.Application.Quizzes.ViewModels;
using CreatingAPI.Application.TicTacToes.ViewModels;
using CreatingAPI.Application.Unscrambles.ViewModels;
using CreatingAPI.Application.Users.ViewModels;
using CreatingAPI.Domain.Activities;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Games;
using CreatingAPI.Domain.Pickers;
using CreatingAPI.Domain.Quizzes;
using CreatingAPI.Domain.Quizzes.ValueObjects;
using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.Unscrambles;
using CreatingAPI.Domain.Users;

namespace CreatingAPI.Application.Core.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Unscramble, UnscrambleViewModel>();
            CreateMap<Exercise, ExerciseViewModel>();
            CreateMap<Bookmark, BookmarkViewModel>();
            CreateMap<Game, GameViewModel>();
            CreateMap<TicTacToe, TicTacToeViewModel>();
            CreateMap<TicTacToeSquare, TicTacToeSquareViewModel>();
            CreateMap<Picker, PickerViewModel>();
            CreateMap<PickerTopic, PickerTopicViewModel>();
            CreateMap<Quiz, QuizViewModel>();
            CreateMap<QuizQuestion, QuizQuestionViewModel>();
            CreateMap<Alternative, AlternativeViewModel>();
            CreateMap<Picker, MyActivitiyViewModel>()
                .ConstructUsing(p => new MyActivitiyViewModel(p.Id, p.Title, p.CreatedAt, p.IsPublic, KindOfActivity.Picker));
            CreateMap<Quiz, MyActivitiyViewModel>()
               .ConstructUsing(q => new MyActivitiyViewModel(q.Id, q.Title, q.CreatedAt, q.IsPublic, KindOfActivity.Quiz));
            CreateMap<Unscramble, MyActivitiyViewModel>()
             .ConstructUsing(u => new MyActivitiyViewModel(u.Id, u.Title, u.CreatedAt, u.IsPublic, KindOfActivity.Unscramble));
            CreateMap<TicTacToe, MyActivitiyViewModel>()
             .ConstructUsing(t => new MyActivitiyViewModel(t.Id, t.Title, t.CreatedAt, t.IsPublic, KindOfActivity.Unscramble));
        }
    }
}
