using DocumentFormat.OpenXml.Drawing.Charts;
using Konet.Application.Common.Interfaces;
using Konet.Application.Common.Models;
using Konet.Application.Identity.Users;
using Konet.Application.Slices.Content.Dtos;
using Konet.Application.Slices.Content.UserVideoStatus.Dtos;
using Konet.Application.Slices.Content.UserVideoStatus.Interfaces;
using Konet.Domain.Content;
using Konet.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Infrastructure.Slices.Content;
public class UserVideoStatusService : IUserVideoStatusService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    protected IQueryable<UserVideoStatusEntity> ActiveUserVideoStatusQuery()
    {
        var userId = _currentUser.GetUserId();
        return _dbContext.UserVideoStatus
            .Where(v => userId.ToString() == v.UserId)
            .Include(v => v.Video)
            .AsSplitQuery();
    }

    public UserVideoStatusService(ApplicationDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<int> CountUserVideoStatuses(CancellationToken cancellationToken) => await ActiveUserVideoStatusQuery().CountAsync(cancellationToken);

    public async Task<List<UserVideoStatusDto>> GetUserVideoStatuses(SimplePaginationFilter request, CancellationToken cancellationToken)
    {
        return await ActiveUserVideoStatusQuery()
          .Skip(request.PageSize * request.PageNumber)
          .Take(request.PageSize)
          .Select(x => new UserVideoStatusDto()
          {
              VideoId = x.VideoId,
              Title = x.Video.Title,
              OwnerId = x.UserId,
              Thumbnail = x.Video.Thumbnail,
              IsDelete = x.Video.IsDelete,
              Status = nameof(x.Status),
          })
          .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<UserVideoStatusEnum> GetStatusOfSpecificUserVideoStatus(DefaultIdType videoId, CancellationToken cancellationToken)
    {
        return await ActiveUserVideoStatusQuery().Select(x => x.Status).FirstOrDefaultAsync(cancellationToken);
    }
}