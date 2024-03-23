using Konet.Application.Slices.Content.UserVideoStatus.Specs;
using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.UserVideoStatus.RequestHandlers;

public class DeleteUserVideoStatusRequest : IRequest<string>
{
    public Guid VideoId { get; set; }
}

public class DeleteUserVideoStatusRequestHandler : IRequestHandler<DeleteUserVideoStatusRequest, string>
{
    private readonly ICurrentUser _currentUser;
    private readonly IRepository<UserVideoStatusEntity> _userVideoStatusRepository;

    public DeleteUserVideoStatusRequestHandler(IRepository<UserVideoStatusEntity> userVideoStatusRepository, ICurrentUser currentUser)
    {
        _userVideoStatusRepository = userVideoStatusRepository;
        _currentUser = currentUser;
    }

    public async Task<string> Handle(DeleteUserVideoStatusRequest request, CancellationToken cancellationToken)
    {
        string userId = _currentUser.GetUserId().ToString();

        var userVideoStatus = await _userVideoStatusRepository.FirstOrDefaultAsync(new UserVideoStatusByUserIdAndVideoIdSpec(userId, request.VideoId), cancellationToken)
            ?? throw new NotFoundException("User video status not found.");
        await _userVideoStatusRepository.DeleteAsync(userVideoStatus, cancellationToken);

        return "Delete user video status successfully";
    }
}