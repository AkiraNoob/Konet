using Konet.Application.Common.Interfaces;
using Konet.Application.Identity.Users;
using Konet.Application.Slices.Content.Dtos;
using Konet.Application.Slices.Content.Interfaces;
using Konet.Application.Slices.Content.UserVideoStatus.Specs;
using Konet.Application.Slices.Content.Video.RequestHandlers;
using Konet.Domain.Content;
using System.Reflection.Metadata;

namespace Konet.Application.Slices.Content.Videos.RequestHandlers;

public class GetDetailVideoRequest : IRequest<VideoWithOwnerInfoDto>
{
    public Guid VideoId { get; set; }
}

public class GetDetailVideoRequestHandler : IRequestHandler<GetDetailVideoRequest, VideoWithOwnerInfoDto>
{
    private readonly IVideoService _videoService;

    public GetDetailVideoRequestHandler(IVideoService videoService)
    {
        _videoService = videoService;
    }

    public async Task<VideoWithOwnerInfoDto> Handle(GetDetailVideoRequest request, CancellationToken cancellationToken)
    {
        return await _videoService.GetDetailVideo(request.VideoId, cancellationToken);
    }
}
