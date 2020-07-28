using CreatingAPI.Domain.Pickers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Pickers.Mapping
{
    public class PickerTopicMap : IEntityTypeConfiguration<PickerTopic>
    {
        public void Configure(EntityTypeBuilder<PickerTopic> builder)
        {
            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Description)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(pt => pt.PickerId)
                .IsRequired();

            builder.Ignore(pt => pt.ValidationErrors);

            builder.ToTable("PickerTopic");
        }
    }
}
