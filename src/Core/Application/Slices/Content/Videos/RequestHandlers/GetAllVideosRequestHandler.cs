using Konet.Application.Slices.Content.Dtos;
using Konet.Application.Slices.Content.Interfaces;

namespace Konet.Application.Slices.Content.Handlers;
public class GetAllVideosRequest : SimplePaginationFilter, IRequest<PaginationResponse<VideoWithOwnerInfoDto>>
{
}

public class GetAllVideosRequestHandler : IRequestHandler<GetAllVideosRequest, PaginationResponse<VideoWithOwnerInfoDto>>
{
    private readonly IVideoService _videoService;
    public GetAllVideosRequestHandler(IVideoService videoService)
    {
        _videoService = videoService;
    }

    public async Task<PaginationResponse<VideoWithOwnerInfoDto>> Handle(GetAllVideosRequest request, CancellationToken cancellationToken)
    {
        var paginationRequest = new SimplePaginationFilter()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
        };

        List<VideoWithOwnerInfoDto> videos = await _videoService.GetVideos(paginationRequest, cancellationToken);
        int total = await _videoService.CountVideos(cancellationToken);

        return new PaginationResponse<VideoWithOwnerInfoDto>(videos, total, request.PageNumber, request.PageSize);
    }
}