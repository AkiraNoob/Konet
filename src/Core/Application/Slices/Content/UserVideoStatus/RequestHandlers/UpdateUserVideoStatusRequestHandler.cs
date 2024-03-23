using CloudinaryDotNet.Actions;
using Konet.Application.Slices.Content.UserVideoStatus.Specs;
using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.UserVideoStatus.RequestHandlers;

public class UpdateUserVideoStatusRequest : IRequest<string>
{
    public Guid VideoId;
    public UserVideoStatusEnum Status;
}

public class UpdateUserVideoStatusRequestHandler : IRequestHandler<UpdateUserVideoStatusRequest, string>
{
    private readonly IRepository<UserVideoStatusEntity> _userVideoStatusRepository;
    private readonly ICurrentUser _currentUser;
    public UpdateUserVideoStatusRequestHandler(IRepository<UserVideoStatusEntity> userVideoStatusRepository, ICurrentUser currentUser)
    {
        _userVideoStatusRepository = userVideoStatusRepository;
        _currentUser = currentUser;
    }

    public async Task<string> Handle(UpdateUserVideoStatusRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.GetUserId();
        var status = await _userVideoStatusRepository.FirstOrDefaultAsync(new UserVideoStatusByUserIdAndVideoIdSpec(userId.ToString(), request.VideoId), cancellationToken);

        if (status is not null)
        {
            status.Status = request.Status;
            await _userVideoStatusRepository.UpdateAsync(status, cancellationToken);
        }
        else
        {
            var newStatus = new UserVideoStatusEntity(userId.ToString(), request.VideoId, request.Status);
            await _userVideoStatusRepository.AddAsync(newStatus, cancellationToken);
        }

        return "Update comment successfully";
    }
}
