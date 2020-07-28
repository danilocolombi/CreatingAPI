using CreatingAPI.Domain.Bookmarks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Bookmarks.Mapping
{
    public class BookmarkMap : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.HasKey(bm => bm.Id);

            builder.Property(bm => bm.UserId)
                .IsRequired();

            builder.Property(bm => bm.UnscrambleId)
                .IsRequired(false);

            builder.Property(bm => bm.TicTacToeId)
                .IsRequired(false);

            builder.Property(bm => bm.PickerId)
               .IsRequired(false);

            builder.Ignore(bm => bm.ValidationErrors);

            builder.ToTable("Bookmark");
        }
    }
}
