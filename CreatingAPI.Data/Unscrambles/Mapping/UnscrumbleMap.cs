using CreatingAPI.Data.Activities.Mapping;
using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.Unscrambles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Unscrambles.Mapping
{
    public class UnscrumbleMap : ActivityMap<Unscramble>
    {
        public override void Configure(EntityTypeBuilder<Unscramble> builder)
        {
            base.Configure(builder);

            builder.HasMany(u => u.Bookmarks)
                .WithOne(bm => bm.Unscramble)
                .HasForeignKey(bm => bm.UnscrambleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Exercises)
                .WithOne(e => e.Unscramble)
                .HasForeignKey(e => e.UnscrambleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Unscramble");
        }
    }
}