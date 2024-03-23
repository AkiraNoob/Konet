using Konet.Application.Slices.Content.Dtos;

namespace Konet.Application.Slices.Content.Interfaces;
public interface IVideoService : ITransientService
{
    Task<List<VideoWithOwnerInfoDto>> GetVideos(SimplePaginationFilter request, CancellationToken cancellationToken);
    Task<VideoWithOwnerInfoDto> GetDetailVideo(Guid videoId, CancellationToken cancellationToken);
    Task<int> CountVideos(CancellationToken cancellationToken);
}
