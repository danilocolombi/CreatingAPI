using CreatingAPI.Application.Activities;
using CreatingAPI.Application.Activities.Interfaces;
using CreatingAPI.Application.Bookmarks;
using CreatingAPI.Application.Bookmarks.Interfaces;
using CreatingAPI.Application.Games;
using CreatingAPI.Application.Games.Interfaces;
using CreatingAPI.Application.Pickers;
using CreatingAPI.Application.Pickers.Interfaces;
using CreatingAPI.Application.Quizzes;
using CreatingAPI.Application.Quizzes.Interfaces;
using CreatingAPI.Application.TicTacToes;
using CreatingAPI.Application.TicTacToes.Interfaces;
using CreatingAPI.Application.Unscrambles;
using CreatingAPI.Application.Unscrambles.Interfaces;
using CreatingAPI.Application.Users;
using CreatingAPI.Application.Users.Interfaces;
using CreatingAPI.Data.Bookmarks.Repository;
using CreatingAPI.Data.Games.Repository;
using CreatingAPI.Data.Pickers.Repository;
using CreatingAPI.Data.Quizzes.Repository;
using CreatingAPI.Data.TicTacToes.Repository;
using CreatingAPI.Data.Unscrumbles.Repository;
using CreatingAPI.Data.Users.Repository;
using CreatingAPI.Domain.Bookmarks.Interfaces;
using CreatingAPI.Domain.Bookmarks.Services;
using CreatingAPI.Domain.Games.Interfaces;
using CreatingAPI.Domain.Games.Services;
using CreatingAPI.Domain.Pickers.Interfaces;
using CreatingAPI.Domain.Pickers.Services;
using CreatingAPI.Domain.Quizzes.Interfaces;
using CreatingAPI.Domain.Quizzes.Services;
using CreatingAPI.Domain.TicTacToes.Interfaces;
using CreatingAPI.Domain.TicTacToes.Services;
using CreatingAPI.Domain.Unscrambles.Interfaces;
using CreatingAPI.Domain.Unscrambles.Services;
using CreatingAPI.Domain.Users.Interfaces;
using CreatingAPI.Domain.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CreatingAPI.Infra.IoC
{
    public class BootStrapper
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IActivityAppService, ActivityAppService>();
            serviceCollection.AddScoped<IBookmarkAppService, BookmarkAppService>();
            serviceCollection.AddScoped<IGameAppService, GameAppService>();
            serviceCollection.AddScoped<IUnscrambleAppService, UnscrambleAppService>();
            serviceCollection.AddScoped<IUserAppService, UserAppService>();
            serviceCollection.AddScoped<ITicTacToeAppService, TicTacToeAppService>();
            serviceCollection.AddScoped<IPickerAppService, PickerAppService>();
            serviceCollection.AddScoped<IQuizAppService, QuizAppService>();

            serviceCollection.AddScoped<IBookmarkService, BookmarkService>();
            serviceCollection.AddScoped<IGameService, GameService>();
            serviceCollection.AddScoped<IUnscrambleService, UnscrumbleService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ITicTacToeService, TicTacToeService>();
            serviceCollection.AddScoped<IPickerService, PickerService>();
            serviceCollection.AddScoped<IQuizService, QuizService>();

            serviceCollection.AddScoped<IBookmarkRepository, BookmarkRepository>();
            serviceCollection.AddScoped<IGameRepository, GameRepository>();
            serviceCollection.AddScoped<IUnscrambleRepository, UnscrambleRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ITicTacToeRepository, TicTacToeRepository>();
            serviceCollection.AddScoped<IPickerRepository, PickerRepository>();
            serviceCollection.AddScoped<IQuizRepository, QuizRepository>();
        }
    }
}
