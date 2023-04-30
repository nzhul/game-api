using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Data.Users;

namespace Server.Data.EntityConfigurations
{
    public class FriendshipConfig : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder
                .HasIndex(fs => new { fs.SenderId, fs.RecieverId })
                .IsUnique();

            builder
                .HasOne(u => u.Sender)
                .WithMany(fs => fs.SendFriendRequests)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(u => u.SenderId);

            builder
                .HasOne(u => u.Reciever)
                .WithMany(fs => fs.RecievedFriendRequests)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(u => u.RecieverId);
        }
    }
}
