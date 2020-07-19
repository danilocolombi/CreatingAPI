using CreatingAPI.Domain.Unscrambles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Unscrambles.Mapping
{
    public class UnscrumbleMap : IEntityTypeConfiguration<Unscramble>
    {
        public void Configure(EntityTypeBuilder<Unscramble> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Bookmarks)
                .WithOne(bm => bm.Unscramble)
                .HasForeignKey(bm => bm.UnscrambleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Exercises)
                .WithOne(e => e.Unscramble)
                .HasForeignKey(e => e.UnscrambleId)
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
            builder.ToTable("Unscramble");
        }
    }
}