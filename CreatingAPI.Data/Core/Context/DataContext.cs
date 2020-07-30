using CreatingAPI.Data.Bookmarks.Mapping;
using CreatingAPI.Data.Games.Mapping;
using CreatingAPI.Data.Pickers.Mapping;
using CreatingAPI.Data.Quizzes.Mapping;
using CreatingAPI.Data.TicTacToes.Mapping;
using CreatingAPI.Data.Unscrambles.Mapping;
using CreatingAPI.Data.Users.Mapping;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Games;
using CreatingAPI.Domain.Pickers;
using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.Unscrambles;
using CreatingAPI.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CreatingAPI.Data.Core.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Unscramble> Unscrambles { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<TicTacToe> TicTacToes { get; set; }
        public DbSet<TicTacToeSquare> TicTacToeSquares { get; set; }
        public DbSet<Picker> Pickers { get; set; }
        public DbSet<PickerTopic> PickerTopics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Creating.Demo;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UnscrumbleMap());
            modelBuilder.ApplyConfiguration(new ExerciseMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new GameMap());
            modelBuilder.ApplyConfiguration(new BookmarkMap());
            modelBuilder.ApplyConfiguration(new TicTacToeMap());
            modelBuilder.ApplyConfiguration(new TicTacToeSquareMap());
            modelBuilder.ApplyConfiguration(new PickerMap());
            modelBuilder.ApplyConfiguration(new PickerTopicMap());
            modelBuilder.ApplyConfiguration(new QuizMap());
            modelBuilder.ApplyConfiguration(new QuizQuestionMap());
        }
    }
}
