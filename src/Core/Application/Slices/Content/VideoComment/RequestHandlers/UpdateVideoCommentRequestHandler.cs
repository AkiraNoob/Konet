using Konet.Application.Slices.Content.VideoComment.Dtos;
using Konet.Domain.Content;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.VideoComment.RequestHandlers;

public class UpdateVideoCommentRequest : IRequest<VideoCommentDto>
{
    public string Content { get; set; } = default!;
    public Guid CommentId { get; set; }
}

public class UpdateVideoCommentRequestHandler : VideoCommentMiddlewareHandler, IRequestHandler<UpdateVideoCommentRequest, VideoCommentDto>
{
    private readonly IRepository<VideoCommentsEntity> _videoCommentEntityRepository;

    public UpdateVideoCommentRequestHandler(IRepository<VideoCommentsEntity> videoCommentEntityRepository, IRepository<VideoEntity> videoRepository)
        : base(videoCommentEntityRepository, videoRepository)
    {
        _videoCommentEntityRepository = videoCommentEntityRepository;
    }

    public async Task<VideoCommentDto> Handle(UpdateVideoCommentRequest request, CancellationToken cancellationToken)
    {
        var comment = await EnsureCommentActive(request.CommentId, cancellationToken);
        return comment.Adapt<VideoCommentDto>();
    }
}
