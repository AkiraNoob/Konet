using Konet.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Domain.Content;
public class VideoCommentsEntity : AuditableEntity, IAggregateRoot
{
    public Guid VideoId { get; set; }
    public string UserId { get; set; }
    public Guid? ParentId { get; set; }
    public string Content { get; set; }
    public VideoCommentsEntity? ParentComment { get; set; }
    public virtual VideoEntity Video { get; private set; } = default!;
    public virtual ICollection<VideoCommentsEntity> VideoReplyComments { get; private set; } = default!;

    public VideoCommentsEntity(string content, Guid videoId, string userId, Guid? parentId)
    {
        this.Content = content;
        this.VideoId = videoId;
        this.UserId = userId;
        this.ParentId = parentId;
    }

    public VideoCommentsEntity Update(string content, Guid actorId)
    {
        this.Content = content;
        this.LastModifiedBy = actorId;
        this.LastModifiedOn = DateTime.UtcNow;
        return this;
    }
}
