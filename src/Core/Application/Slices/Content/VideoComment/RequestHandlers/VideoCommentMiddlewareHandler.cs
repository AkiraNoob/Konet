using Konet.Application.Slices.Content.VideoComment.Specs;
using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.VideoComment.RequestHandlers;
public class VideoCommentMiddlewareHandler
{
    protected readonly IRepository<VideoCommentsEntity> _videoCommentsRepository;
    protected readonly IRepository<VideoEntity> _videoEntityRepository;

    public VideoCommentMiddlewareHandler(IRepository<VideoCommentsEntity> videoCommentsRepository, IRepository<VideoEntity> videoEntityRepository)
    {
        _videoCommentsRepository = videoCommentsRepository;
        _videoEntityRepository = videoEntityRepository;
    }

    protected async Task<VideoCommentsEntity> EnsureCommentActive(Guid commentId, CancellationToken cancellationToken)
    {
        var comment = await _videoCommentsRepository.FirstOrDefaultAsync(new VideoByCommentIdSpec(commentId), cancellationToken) ?? throw new NotFoundException("Comment not found.");
        if (comment.Video.IsDelete)
        {
            throw new NotFoundException("Comment not found.");
        }

        return comment;
    }
}