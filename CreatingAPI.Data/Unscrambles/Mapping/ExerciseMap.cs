using CreatingAPI.Domain.Unscrambles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Unscrambles.Mapping
{
    public class ExerciseMap : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(e => e.Position)
                .IsRequired()
                .HasColumnType("TINYINT");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.UnscrambleId)
                .IsRequired();

            builder.Ignore(e => e.ValidationErrors);

            builder.ToTable("Exercise");
        }
    }
}
