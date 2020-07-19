using CreatingAPI.Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Games.Mapping
{
    public class GameMap : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.EndedAt)
                .IsRequired();

            builder.Property(g => g.StartedAt)
                .IsRequired();

            builder.Property(g => g.NumberOfCorrectAnswers)
                .IsRequired()
                .HasColumnType("TINYINT");

            builder.Property(g => g.NumberOfWrongAnswers)
                .IsRequired()
                .HasColumnType("TINYINT");

            builder.Property(g => g.UnscrambleId)
                .IsRequired();

            builder.Property(g => g.UserId)
                .IsRequired();

            builder.Ignore(g => g.ValidationErrors);
            builder.ToTable("Game");
        }
    }
}
