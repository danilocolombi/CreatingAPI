using CreatingAPI.Data.Activities.Mapping;
using CreatingAPI.Domain.TicTacToes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.TicTacToes.Mapping
{
    public class TicTacToeMap : ActivityMap<TicTacToe>
    {
        public override void Configure(EntityTypeBuilder<TicTacToe> builder)
        {
            base.Configure(builder);

            builder.HasMany(ttt => ttt.Squares)
                .WithOne(ts => ts.TicTacToe)
                .HasForeignKey(ts => ts.TicTacToeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Bookmarks)
             .WithOne(bm => bm.TicTacToe)
             .HasForeignKey(bm => bm.TicTacToeId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("TicTacToe");
        }
    }
}
