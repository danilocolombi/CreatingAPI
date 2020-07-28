using CreatingAPI.Domain.TicTacToes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.TicTacToes.Mapping
{
    public class TicTacToeSquareMap : IEntityTypeConfiguration<TicTacToeSquare>
    {
        public void Configure(EntityTypeBuilder<TicTacToeSquare> builder)
        {
            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.Position)
                .IsRequired()
                .HasColumnType("TINYINT");

            builder.Property(ts => ts.Description)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(ts => ts.TicTacToeId)
                .IsRequired();

            builder.Ignore(ts => ts.ValidationErrors);

            builder.ToTable("TicTacToeSquare");
        }
    }
}
