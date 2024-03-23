using Konet.Application.Common.Enums;
using Konet.Application.Slices.Accessibility.Interfaces;
using Konet.Application.Slices.Content.VideoComment.RequestHandlers;
using Konet.Domain.Content;

namespace Konet.Application.Slices.Content.VideoComment.Handlers;

public class DeleteVideoCommentRequest : IRequest<string>
{
    public Guid CommentId { get; set; }
}

public class DeleteVideoCommentRequestHandler : VideoCommentMiddlewareHandler, IRequestHandler<DeleteVideoCommentRequest, string>
{
    private readonly IRepository<VideoCommentsEntity> _videoCommentRepository;
    private readonly IContentAccessibility _contentAccessibility;
    private readonly ICurrentUser _currentUser;

    public DeleteVideoCommentRequestHandler(
        IRepository<VideoCommentsEntity> videoCommentRepository,
        IRepository<VideoEntity> videoRepository,
        IContentAccessibility contentAccessibility,
        ICurrentUser currentUser)
        : base(videoCommentRepository, videoRepository)
    {
        _videoCommentRepository = videoCommentRepository;
        _contentAccessibility = contentAccessibility;
        _currentUser = currentUser;
    }

    public async Task<string> Handle(DeleteVideoCommentRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.GetUserId();

        var comment = await EnsureCommentActive(request.CommentId, cancellationToken);

        CommentPermissionEnum commentPermission = _contentAccessibility.GetCommentPermisson(comment, userId, cancellationToken);
        VideoPermissionEnum videoPermission = _contentAccessibility.GetVideoPermission(comment.Video, userId, cancellationToken);

        if(commentPermission is not CommentPermissionEnum.Owned || videoPermission is not VideoPermissionEnum.Owned)
        {
            throw new ForbiddenException("You are not allowed to delete this comment.");
        }

        await _videoCommentRepository.DeleteAsync(comment, cancellationToken);

        var childComments = await _videoCommentRepository.ListAsync(new DeleteChildrenCommentSpec(comment.Id), cancellationToken);
        await _videoCommentRepository.DeleteRangeAsync(childComments, cancellationToken);

        return "Delete comment successfully";
    }
}

public class DeleteChildrenCommentSpec : Specification<VideoCommentsEntity>
{
    public DeleteChildrenCommentSpec(Guid commentId)
    {
        Query.Where(c => c.ParentId == commentId);
    }
}