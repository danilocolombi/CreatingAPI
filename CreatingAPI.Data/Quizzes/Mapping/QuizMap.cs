using CreatingAPI.Data.Activities.Mapping;
using CreatingAPI.Domain.Quizzes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Quizzes.Mapping
{
    public class QuizMap : ActivityMap<Quiz>
    {
        public override void Configure(EntityTypeBuilder<Quiz> builder)
        {
            base.Configure(builder);

            builder.HasMany(qz => qz.Questions)
                .WithOne(ts => ts.Quiz)
                .HasForeignKey(ts => ts.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Bookmarks)
             .WithOne(bm => bm.Quiz)
             .HasForeignKey(bm => bm.QuizId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Quiz");
        }
    }
}
