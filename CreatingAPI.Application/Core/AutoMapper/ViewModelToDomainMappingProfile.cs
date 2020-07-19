using AutoMapper;
using CreatingAPI.Application.Bookmarks.ViewModels;
using CreatingAPI.Application.Games.ViewModels;
using CreatingAPI.Application.TicTacToes.ViewModels;
using CreatingAPI.Application.Unscrambles.ViewModels;
using CreatingAPI.Application.Users.ViewModels;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Games;
using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.Unscrambles;
using CreatingAPI.Domain.Users;

namespace CreatingAPI.Application.Core.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserCreationViewModel, User>()
                .ConvertUsing(u => new User(u.Name, u.Email, u.Password));

            CreateMap<BookmarkCreationViewModel, Bookmark>()
                .ConvertUsing(b => new Bookmark(b.UserId, b.UnscrumbleId, b.KindOfActivity));

            CreateMap<GameCreationViewModel, Game>()
                .ConstructUsing(g => new Game(g.UserId, g.UnscrumbleId, g.StartedAt, g.EndedAt, g.NumberOfCorrectAnswers, g.NumberOfWrongAnswers));

            CreateMap<ExerciseViewModel, Exercise>()
                .ConstructUsing(e => new Exercise(e.Description, e.Position));

            CreateMap<UnscrambleCreationViewModel, Unscramble>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(u => new Unscramble(u.Title, u.UserId, u.IsPublic));

            CreateMap<TicTacToeCreationViewModel, TicTacToe>()
              .IgnoreAllPropertiesWithAnInaccessibleSetter()
              .ConstructUsing(u => new TicTacToe(u.Title, u.UserId, u.IsPublic));

            CreateMap<TicTacToeSquareViewModel, TicTacToeSquare>()
               .ConstructUsing(e => new TicTacToeSquare(e.Description, e.Position));

        }
    }
}
