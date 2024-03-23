using Konet.Application.Common.Enums;
using Konet.Application.Slices.Accessibility.Interfaces;
using Konet.Application.Slices.Content.Video.RequestHandlers;
using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content;
public class SoftDeleteVideoRequest : IRequest<string>
{
    public Guid VideoId { get; set; } = default!;
}

public class SoftDeleteVideoRequestHandler : VideoMiddlewareHandler, IRequestHandler<SoftDeleteVideoRequest, string>
{
    private readonly IRepository<VideoEntity> _repository;
    private readonly ICurrentUser _currentUser;
    private readonly IContentAccessibility _contentAccessibility;

    public SoftDeleteVideoRequestHandler(IRepository<VideoEntity> repository, ICurrentUser currentUser, IContentAccessibility contentAccessibility)
        : base(repository)
    {
        _repository = repository;
        _currentUser = currentUser;
        _contentAccessibility = contentAccessibility;
    }

    public async Task<string> Handle(SoftDeleteVideoRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.GetUserId();
        VideoEntity video = await EnsureVideoActive(request.VideoId, cancellationToken);

        VideoPermissionEnum videoPermission = _contentAccessibility.GetVideoPermission(video, userId, cancellationToken);

        if(videoPermission is not VideoPermissionEnum.Owned)
        {
            throw new ForbiddenException("You are not allowed to move this video to trash");
        }

        video.SoftDelete(userId);
        await _repository.UpdateAsync(video, cancellationToken);

        return "Move videos to trash";
    }
}