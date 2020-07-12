using AutoMapper;
using CreatingAPI.Application.Bookmarks.ViewModels;
using CreatingAPI.Application.Games.ViewModels;
using CreatingAPI.Application.TicTacToes.ViewModels;
using CreatingAPI.Application.Unscrumbles.ViewModels;
using CreatingAPI.Application.Users.ViewModels;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Games;
using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.Unscrumbles;
using CreatingAPI.Domain.Users;

namespace CreatingAPI.Application.Core.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Unscrumble, UnscrumbleViewModel>();
            CreateMap<Exercise, ExerciseViewModel>();
            CreateMap<Bookmark, BookmarkViewModel>();
            CreateMap<Game, GameViewModel>();
            CreateMap<TicTacToe, TicTacToeViewModel>();
            CreateMap<TicTacToeSquare, TicTacToeSquareViewModel>();
        }
    }
}
