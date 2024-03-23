using Konet.Application.Slices.Content.UserVideoStatus.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.UserVideoStatus.Interfaces;
public interface IUserVideoStatusService : ITransientService
{
    public Task<List<UserVideoStatusDto>> GetUserVideoStatuses(SimplePaginationFilter paginationFilter, CancellationToken cancellationToken);
    public Task<int> CountUserVideoStatuses(CancellationToken cancellationToken);
    public Task<UserVideoStatusEnum> GetStatusOfSpecificUserVideoStatus(Guid videoId, CancellationToken cancellationToken);
}
