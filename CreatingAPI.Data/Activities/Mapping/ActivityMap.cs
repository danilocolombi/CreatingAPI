using CreatingAPI.Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Activities.Mapping
{
    public abstract class ActivityMap<TActivity> : IEntityTypeConfiguration<TActivity> where TActivity : Activity
    {
        public virtual void Configure(EntityTypeBuilder<TActivity> builder)
        {
            builder.HasKey(act => act.Id);

            builder.Property(act => act.CreatedAt)
               .IsRequired();

            builder.Property(act => act.IsPublic)
                .IsRequired();

            builder.Property(act => act.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(act => act.UserId)
                .IsRequired();

            builder.Ignore(ttt => ttt.ValidationErrors);
        }
    }
}
