using Konet.Application.Slices.Content.Dtos;
using Konet.Application.Slices.Content.Video.RequestHandlers;
using Konet.Domain.Content;
using Mapster;

namespace Konet.Application.Slices.Content.Handlers;

public class UpdateVideoRequest : IRequest<VideoDto>
{
    public string Title { get; set; } = default!;
    public Guid VideoId { get; set; }
}

public class UpdateVideoRequestHandler : VideoMiddlewareHandler, IRequestHandler<UpdateVideoRequest, VideoDto>
{
    private readonly IRepository<VideoEntity> _repository;

    public UpdateVideoRequestHandler(IRepository<VideoEntity> repository)
        : base(repository)
    {
        _repository = repository;
    }

    public async Task<VideoDto> Handle(UpdateVideoRequest request, CancellationToken cancellationToken)
    {
        VideoEntity video = await EnsureVideoActive(request.VideoId, cancellationToken);

        video.Title = request.Title;

        await _repository.UpdateAsync(video, cancellationToken);
        return video.Adapt<VideoDto>();
    }
}
