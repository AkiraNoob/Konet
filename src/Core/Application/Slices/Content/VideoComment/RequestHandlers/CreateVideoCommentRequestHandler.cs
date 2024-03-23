using Konet.Application.Slices.Content.Handlers;
using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.VideoComment.Handlers;

public class CreatVideoCommentRequest : IRequest<Guid>
{
    public string Content { get; set; } = default!;
    public Guid VideoId { get; set; }
    public Guid? ParentCommentId { get; set; }
}

public class CreateVideoCommentRequestHandler : IRequestHandler<CreatVideoCommentRequest, Guid>
{
    private readonly IRepository<VideoCommentsEntity> _repository;
    private readonly ICurrentUser _currentUser;

    public CreateVideoCommentRequestHandler(ICurrentUser currentUser, IRepository<VideoCommentsEntity> repository)
    {
        _currentUser = currentUser;
        _repository = repository;
    }

    public async Task<Guid> Handle(CreatVideoCommentRequest request, CancellationToken cancellationToken)
    {
        string userId = _currentUser.GetUserId().ToString();
        var comment = new VideoCommentsEntity(request.Content, request.VideoId, userId, request.ParentCommentId);

        await _repository.AddAsync(comment, cancellationToken);

        return comment.Id;
    }
}
