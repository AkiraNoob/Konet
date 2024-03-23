using CloudinaryDotNet.Actions;
using Konet.Application.Common.Enums;
using Konet.Application.Slices.Accessibility.Interfaces;
using Konet.Application.Slices.Content.Video.RequestHandlers;
using Konet.Domain.Common.Events;
using Konet.Domain.Content;
using Konet.Shared.Events;

namespace Konet.Application.Slices.Content;
public class HardDeleteVideoRequest : IRequest<string>
{
    public Guid VideoId { get; set; } = default!;
}

public class HardDeleteVideoRequestHandler : VideoMiddlewareHandler, IRequestHandler<HardDeleteVideoRequest, string>
{
    private readonly IStorageService _storageService;
    private readonly IRepository<VideoEntity> _repository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IContentAccessibility _contentAccessibility;
    private readonly ICurrentUser _currentUser;

    public HardDeleteVideoRequestHandler(IRepository<VideoEntity> repository, IStorageService storageService, IEventPublisher eventPublisher, IContentAccessibility contentAccessibility, ICurrentUser currentUser)
        : base(repository)
    {
        _storageService = storageService;
        _repository = repository;
        _eventPublisher = eventPublisher;
        _contentAccessibility = contentAccessibility;
        _currentUser = currentUser;
    }

    public async Task<string> Handle(HardDeleteVideoRequest request, CancellationToken cancellationToken)
    {
        var video = await EnsureVideoExist(request.VideoId, cancellationToken);
        var userId = _currentUser.GetUserId();

        VideoPermissionEnum videoPermission = _contentAccessibility.GetVideoPermission(video, userId, cancellationToken);
        if(videoPermission is not VideoPermissionEnum.Owned)
        {
            throw new ForbiddenException("You are not allowed to permanently delete this resource.");
        }

        List<Task> tasks = new List<Task>
        {
            _storageService.DeleteAsync(video.PublicFileId, ResourceType.Video),
            _eventPublisher.PublishAsync(EntityDeletedEvent.WithEntity(video))
        };

        await Task.WhenAll(tasks);

        await _repository.DeleteAsync(video, cancellationToken);

        return "Delete video successfully";
    }
}
