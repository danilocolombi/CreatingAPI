using CreatingAPI.Domain.Quizzes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Quizzes.Mapping
{
    public class QuizQuestionMap : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.HasKey(qq => qq.Id);

            builder.Property(qq => qq.Description)
           .IsRequired()
           .HasMaxLength(150);

            builder.Property(qq => qq.QuizId)
                .IsRequired();

            builder.Ignore(qq => qq.ValidationErrors);

            builder.OwnsOne(u => u.AlternativeA, alternative =>
            {
                alternative.Property(u => u.Description)
                .IsRequired()
                .HasColumnName("AlternativeA")
                .HasMaxLength(150);

                alternative.Property(u => u.IsCorrect)
                 .HasColumnName("IsAlternativeACorrect")
                 .HasMaxLength(150);
            });

            builder.OwnsOne(u => u.AlternativeB, alternative =>
            {
                alternative.Property(u => u.Description)
                .IsRequired()
                .HasColumnName("AlternativeB")
                .HasMaxLength(150);

                alternative.Property(u => u.IsCorrect)
                 .HasColumnName("IsAlternativeBCorrect");
            });

            builder.OwnsOne(u => u.AlternativeC, alternative =>
            {
                alternative.Property(u => u.Description)
                .IsRequired(false)
                .HasColumnName("AlternativeC")
                .HasMaxLength(150);

                alternative.Property(u => u.IsCorrect)
                .HasColumnName("IsAlternativeCCorrect");
            });

            builder.OwnsOne(u => u.AlternativeD, alternative =>
            {
                alternative.Property(u => u.Description)
                .IsRequired(false)
                .HasColumnName("AlternativeD")
                .HasMaxLength(150);

                alternative.Property(u => u.IsCorrect)
                 .HasColumnName("IsAlternativeDCorrect");
            });

            builder.ToTable("QuizQuestion");
        }
    }
}
