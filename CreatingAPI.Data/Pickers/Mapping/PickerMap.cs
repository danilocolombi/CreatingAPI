using CreatingAPI.Data.Activities.Mapping;
using CreatingAPI.Domain.Pickers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Pickers.Mapping
{
    public class PickerMap : ActivityMap<Picker>
    {
        public override void Configure(EntityTypeBuilder<Picker> builder)
        {
            base.Configure(builder);

            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Topics)
                .WithOne(pt => pt.Picker)
                .HasForeignKey(pt => pt.PickerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Bookmarks)
             .WithOne(bm => bm.Picker)
             .HasForeignKey(bm => bm.PickerId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Picker");
        }
    }
}
