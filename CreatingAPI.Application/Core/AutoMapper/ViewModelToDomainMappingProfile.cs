using AutoMapper;
using CreatingAPI.Application.Bookmarks.ViewModels;
using CreatingAPI.Application.Games.ViewModels;
using CreatingAPI.Application.Pickers.ViewModels;
using CreatingAPI.Application.TicTacToes.ViewModels;
using CreatingAPI.Application.Unscrambles.ViewModels;
using CreatingAPI.Application.Users.ViewModels;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Games;
using CreatingAPI.Domain.Pickers;
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
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConvertUsing(u => new User(u.Name, u.Email, u.Password));

            CreateMap<BookmarkCreationViewModel, Bookmark>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConvertUsing(b => new Bookmark(b.UserId, b.UnscrumbleId, b.KindOfActivity));

            CreateMap<GameCreationViewModel, Game>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(g => new Game(g.UserId, g.UnscrumbleId, g.StartedAt, g.EndedAt, g.NumberOfCorrectAnswers, g.NumberOfWrongAnswers));

            CreateMap<ExerciseViewModel, Exercise>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(e => new Exercise(e.Description, e.Position));

            CreateMap<UnscrambleCreationViewModel, Unscramble>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(u => new Unscramble(u.Title, u.UserId, u.IsPublic));

            CreateMap<TicTacToeCreationViewModel, TicTacToe>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(u => new TicTacToe(u.Title, u.UserId, u.IsPublic));

            CreateMap<TicTacToeSquareViewModel, TicTacToeSquare>()
               .IgnoreAllPropertiesWithAnInaccessibleSetter()
               .ConstructUsing(ts => new TicTacToeSquare(ts.Description, ts.Position));

            CreateMap<PickerCreationViewModel, Picker>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
              .ConstructUsing(p => new Picker(p.Title, p.UserId, p.IsPublic));

            CreateMap<PickerTopicViewModel, PickerTopic>()
               .IgnoreAllPropertiesWithAnInaccessibleSetter()
               .ConstructUsing(pt => new PickerTopic(pt.Description));
        }
    }
}
