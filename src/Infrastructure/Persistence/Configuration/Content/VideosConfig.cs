using Konet.Domain.Content;
using Konet.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Konet.Infrastructure.Persistence.Configuration.Content;
public class VideosConfig : IEntityTypeConfiguration<VideoEntity>
{
    public void Configure(EntityTypeBuilder<VideoEntity> builder)
    {
        builder.ToTable("Videos", SchemaNames.Konet);
        builder.Property(u => u.Title).HasMaxLength(256);
        builder.Property(u => u.Url).HasMaxLength(256);
        builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId);
        builder.HasMany(u => u.Comments).WithOne(u => u.Video).HasForeignKey(u => u.VideoId);
        builder.HasMany(u => u.UserVideo).WithOne(u => u.Video).HasForeignKey(u => u.VideoId);
    }
}
