using CloudinaryDotNet;
using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.Video.RequestHandlers;
public class VideoMiddlewareHandler
{
    protected readonly IRepository<VideoEntity> _videosRepository;

    protected VideoMiddlewareHandler(IRepository<VideoEntity> videosRepository)
    {
        _videosRepository = videosRepository;
    }

    protected async Task<VideoEntity> EnsureVideoExist(DefaultIdType videoId, CancellationToken cancellationToken)
    {
        return await _videosRepository.FirstOrDefaultAsync(new GetVideoExistByIdSpec(videoId), cancellationToken)
                     ?? throw new NotFoundException("Video not found.");
    }

    protected async Task<VideoEntity> EnsureVideoActive(DefaultIdType videoId, CancellationToken cancellationToken)
    {
        return await _videosRepository.FirstOrDefaultAsync(new GetActiveVideoByIdSpec(videoId), cancellationToken)
                     ?? throw new NotFoundException("Video not found.");
    }
}

internal class GetVideoExistByIdSpec : Specification<VideoEntity>
{
    public GetVideoExistByIdSpec(DefaultIdType videoId)
    {
        Query.Where(x => x.Id == videoId);
    }
}

internal class GetActiveVideoByIdSpec : Specification<VideoEntity>
{
    public GetActiveVideoByIdSpec(DefaultIdType videoId)
    {
        Query.Where(x => x.Id == videoId && !x.IsDelete);
    }
}