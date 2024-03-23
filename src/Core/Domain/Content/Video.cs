using Konet.Domain.Identity;

namespace Konet.Domain.Content;
public class VideoEntity : AuditableEntity, IAggregateRoot
{
    public string Url { get; set; }
    public string? Thumbnail { get; set; }
    public string Title { get; set; }
    public string UserId { get; set; }
    public bool IsDelete { get; set; } = false;
    public string PublicFileId { get; set; } = default!;
    public virtual ICollection<VideoCommentsEntity> Comments { get; private set; } = default!;
    public virtual ICollection<UserVideoStatusEntity> UserVideo { get; private set; } = default!;

    public VideoEntity(string url, string title, string userId, string publicFileId = "")
    {
        this.Url = url;
        this.Title = title;
        this.UserId = userId;
        this.PublicFileId = publicFileId;
    }

    public VideoEntity Update(string title, Guid modifiedById)
    {
        this.Title = title;

        this.LastModifiedOn = DateTime.UtcNow;
        this.LastModifiedBy = modifiedById;

        return this;
    }

    public VideoEntity SoftDelete(Guid actorId)
    {
        this.DeletedBy = actorId;
        this.DeletedOn = DateTime.UtcNow;
        this.IsDelete = true;
        return this;
    }

    public VideoEntity Restore(Guid actorId)
    {
        this.IsDelete = false;
        this.LastModifiedOn = DateTime.UtcNow;
        this.LastModifiedBy = actorId;
        return this;
    }
}
