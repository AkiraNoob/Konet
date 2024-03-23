using Konet.Domain.Content;
using Konet.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Infrastructure.Persistence.Configuration.Content;
public class UserVideoStatusConfig : IEntityTypeConfiguration<UserVideoStatusEntity>
{
    public void Configure(EntityTypeBuilder<UserVideoStatusEntity> builder)
    {
        builder.ToTable("UserVideoStatus", SchemaNames.Konet);
        builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(u => u.UserId);
        builder.HasOne(uvs => uvs.Video).WithMany(v => v.UserVideo).HasForeignKey(uvs => uvs.VideoId);
        builder.HasIndex(uvs => new { uvs.UserId, uvs.VideoId, uvs.Status }).IsUnique();
    }
}
