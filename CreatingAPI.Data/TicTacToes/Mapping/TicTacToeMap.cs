using CreatingAPI.Domain.TicTacToes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.TicTacToes.Mapping
{
    public class TicTacToeMap : IEntityTypeConfiguration<TicTacToe>
    {
        public void Configure(EntityTypeBuilder<TicTacToe> builder)
        {
            builder.HasKey(ttt => ttt.Id);

            builder.HasMany(ttt => ttt.Squares)
                .WithOne(ts => ts.TicTacToe)
                .HasForeignKey(ts => ts.TicTacToeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Bookmarks)
             .WithOne(bm => bm.TicTacToe)
             .HasForeignKey(bm => bm.TicTacToeId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ttt => ttt.CreatedAt)
                .IsRequired();

            builder.Property(ttt => ttt.IsPublic)
                .IsRequired();

            builder.Property(ttt => ttt.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(ttt => ttt.UserId)
                .IsRequired();

            builder.Ignore(u => u.ValidationErrors);
            builder.ToTable("TicTacToe");
        }
    }
}
