using CreatingAPI.Domain.Unscrumbles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Unscrumbles.Mapping
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

            builder.Property(e => e.UnscrumbleId)
                .IsRequired();

            builder.Ignore(g => g.ValidationErrors);
            builder.ToTable("Exercise");
        }
    }
}
