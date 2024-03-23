using Ardalis.Specification;
using Konet.Application.Common.Exceptions;
using Konet.Application.Common.Interfaces;
using Konet.Application.Common.Models;
using Konet.Application.Identity.Users;
using Konet.Application.Slices.Content.Dtos;
using Konet.Application.Slices.Content.Interfaces;
using Konet.Application.Slices.Shared.Dtos;
using Konet.Domain.Content;
using Konet.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Konet.Infrastructure.Slices.Content;
public class VideoService : IVideoService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserService _userService;
    private readonly ICurrentUser _currentUser;

    public VideoService(ApplicationDbContext dbContext, IUserService userService, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _userService = userService;
        _currentUser = currentUser;
    }

    protected IQueryable<VideoEntity> ActiveVideoQuery(IQueryable<VideoEntity>? query = null)
    {
        if (query is not null)
        {
            return query.Where(v => !v.IsDelete).Include(v => v.UserVideo);
        }

        return _dbContext.Video.Where(v => !v.IsDelete).Include(v => v.UserVideo);
    }

    public async Task<int> CountVideos(CancellationToken cancellationToken) => await ActiveVideoQuery().CountAsync(cancellationToken);

    public async Task<List<VideoWithOwnerInfoDto>> GetVideos(SimplePaginationFilter request, CancellationToken cancellationToken)
    {
        List<VideoDto> videos = await ActiveVideoQuery()
            .Skip(request.PageSize * request.PageNumber)
            .Take(request.PageSize)
            .Select(x => new VideoDto()
            {
                Id = x.Id,
                Title = x.Title,
                Url = x.Url,
                OwnerId = x.CreatedBy.ToString(),
                Thumbnail = x.Thumbnail,
                VideoStatus = x.UserVideo.FirstOrDefault(v => v.UserId == _currentUser.GetUserId().ToString()).Status
            })
            .ToListAsync(cancellationToken: cancellationToken);

        var userIds = videos.ConvertAll(v => v.OwnerId);
        var userInfos = await _userService.GetListAsync(userIds, cancellationToken);

        List<VideoWithOwnerInfoDto> response = new List<VideoWithOwnerInfoDto>();

        videos.ForEach(v =>
        {
            var userInfo = userInfos.FirstOrDefault(x => x.Id.ToString() == v.OwnerId);
            response.Add(new VideoWithOwnerInfoDto()
            {
                Id = v.Id,
                Title = v.Title,
                Url = v.Url,
                OwnerId = v.OwnerId,
                VideoStatus = v.VideoStatus,
                OwnerVideo = new OwnerInfoDto()
                {
                    Id = v.OwnerId,
                    FullName = userInfo.FirstName + userInfo.LastName,
                    Avatar = userInfo.ImageUrl
                }
            });
        });

        return response;
    }

    public async Task<VideoWithOwnerInfoDto> GetDetailVideo(DefaultIdType videoId, CancellationToken cancellationToken)
    {
        var video = await ActiveVideoQuery().Select(x => new VideoDto()
        {
            Id = x.Id,
            Title = x.Title,
            Url = x.Url,
            OwnerId = x.CreatedBy.ToString(),
            Thumbnail = x.Thumbnail,
            VideoStatus = x.UserVideo.FirstOrDefault(v => v.UserId == _currentUser.GetUserId().ToString()).Status
        }).FirstOrDefaultAsync(x => x.Id == videoId, cancellationToken) ?? throw new NotFoundException("Video not found.");

        var userInfo = await _userService.GetAsync(video.OwnerId, cancellationToken);

        return new VideoWithOwnerInfoDto()
        {
            Id = video.Id,
            Title = video.Title,
            Url = video.Url,
            OwnerId = video.OwnerId,
            VideoStatus = video.VideoStatus,
            OwnerVideo = new OwnerInfoDto()
            {
                Id = video.OwnerId,
                FullName = userInfo.FirstName + userInfo.LastName,
                Avatar = userInfo.ImageUrl
            }
        };
    }
}

public class GetAllUserVideoStatusSpec : Specification<UserVideoStatusEntity>
{
    public GetAllUserVideoStatusSpec(string userId)
    {
        Query.Where(x => x.UserId == userId);
    }
}