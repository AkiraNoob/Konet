using Konet.Domain.Content;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Konet.Infrastructure.Identity;

namespace Konet.Infrastructure.Persistence.Configuration.Content;
public class VideoCommentsConfig : IEntityTypeConfiguration<VideoCommentsEntity>
{
    public void Configure(EntityTypeBuilder<VideoCommentsEntity> builder)
    {
        builder.ToTable("VideoComments", SchemaNames.Konet);
        builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId);
        builder.HasOne(u => u.Video).WithMany(u => u.Comments).HasForeignKey(u => u.VideoId);
        builder.HasMany(u => u.VideoReplyComments).WithOne(x => x.ParentComment).HasForeignKey(x => x.ParentId);
    }
}