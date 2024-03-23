using Konet.Application.Slices.Shared.Dtos;

namespace Konet.Application.Slices.Content.VideoComment.Dtos;
public class VideoCommentDto
{
    public Guid Id { get; set; }
    public Guid VideoId { get; set; }
    public string Content { get; set; } = default!;
    public Guid? ParentId { get; set; }
    public DateTime CreatedOn { get; set; }
}

public class VideoCommentWithOwnerInfoDto : VideoCommentDto
{
    public OwnerInfoDto OwnerVideo { get; set; } = default!;
}
