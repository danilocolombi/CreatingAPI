using CreatingAPI.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingAPI.Data.Users.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasMany(u => u.Unscrambles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.Bookmarks)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.Games)
                .WithOne(g => g.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(u => u.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasMaxLength(150);
            });

            builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(u => u.Characters)
                .IsRequired()
                .HasColumnName("Password")
                .HasMaxLength(150);
            });

            builder.HasMany(u => u.TicTacToes)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.Pickers)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(u => u.ValidationErrors);

            builder.ToTable("User");
        }
    }
}
