using AutoMapper;
using CreatingAPI.Application.Bookmarks.ViewModels;
using CreatingAPI.Application.Games.ViewModels;
using CreatingAPI.Application.Unscrumbles.ViewModels;
using CreatingAPI.Application.Users.ViewModels;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Games;
using CreatingAPI.Domain.Unscrumbles;
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
                .ConvertUsing(b => new Bookmark(b.UserId, b.UnscrumbleId));
            CreateMap<GameCreationViewModel, Game>()
                .ConstructUsing(g => new Game(g.UserId, g.UnscrumbleId, g.StartedAt, g.EndedAt, g.NumberOfCorrectAnswers, g.NumberOfWrongAnswers));
            CreateMap<ExerciseViewModel, Exercise>()
                .ConstructUsing(e => new Exercise(e.Description, e.Position));
            CreateMap<UnscrumbleCreationViewModel, Unscrumble>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(u => new Unscrumble(u.Title, u.UserId, u.IsPublic));
        }
    }
}
