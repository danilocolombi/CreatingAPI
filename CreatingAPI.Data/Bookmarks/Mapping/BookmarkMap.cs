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

            builder.Property(bm => bm.UnscrumbleId)
                .IsRequired();

            builder.Ignore(bm => bm.ValidationErrors);

            builder.ToTable("Bookmark");
        }
    }
}
