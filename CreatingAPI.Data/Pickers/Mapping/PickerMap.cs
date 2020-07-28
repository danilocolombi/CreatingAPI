using CreatingAPI.Domain.Pickers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Pickers.Mapping
{
    public class PickerMap : IEntityTypeConfiguration<Picker>
    {
        public void Configure(EntityTypeBuilder<Picker> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Topics)
                .WithOne(pt => pt.Picker)
                .HasForeignKey(pt => pt.PickerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Bookmarks)
             .WithOne(bm => bm.Picker)
             .HasForeignKey(bm => bm.PickerId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.IsPublic)
                .IsRequired();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Ignore(p => p.ValidationErrors);

            builder.ToTable("Picker");
        }
    }
}
