using CreatingAPI.Domain.Unscrumbles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Unscrumbles.Mapping
{
    public class UnscrumbleMap : IEntityTypeConfiguration<Unscrumble>
    {
        public void Configure(EntityTypeBuilder<Unscrumble> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Bookmarks)
                .WithOne(bm => bm.Unscrumble)
                .HasForeignKey(bm => bm.UnscrumbleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Exercises)
                .WithOne(e => e.Unscrumble)
                .HasForeignKey(e => e.UnscrumbleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.IsPublic)
                .IsRequired();

            builder.Property(u => u.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.UserId)
                .IsRequired();

            builder.Ignore(u => u.ValidationErrors);
            builder.ToTable("Unscrumble");
        }
    }
}