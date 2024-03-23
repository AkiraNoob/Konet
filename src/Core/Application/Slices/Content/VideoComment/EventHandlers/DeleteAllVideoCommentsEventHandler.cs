using Konet.Application.Slices.Content.VideoComment.Specs;
using Konet.Domain.Common.Events;
using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.VideoComment.EventHandlers;
public class DeleteAllVideoCommentsEventHandler : EventNotificationHandler<EntityDeletedEvent<VideoEntity>>
{
    private readonly IRepository<VideoCommentsEntity> _repository;

    public DeleteAllVideoCommentsEventHandler(IRepository<VideoCommentsEntity> repository)
    {
        _repository = repository;
    }

    public override async Task Handle(EntityDeletedEvent<VideoEntity> @event, CancellationToken cancellationToken)
    {
        var video = @event.Entity;

        List<VideoCommentsEntity> videoComments = await _repository.ListAsync(new CommentsByVideoIdSpec(video.Id), cancellationToken);

        await _repository.DeleteRangeAsync(videoComments, cancellationToken);
    }
}
