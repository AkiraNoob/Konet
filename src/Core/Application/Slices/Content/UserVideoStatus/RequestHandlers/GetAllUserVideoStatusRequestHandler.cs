using Konet.Application.Slices.Content.UserVideoStatus.Dtos;
using Konet.Application.Slices.Content.UserVideoStatus.Interfaces;

namespace Konet.Application.Slices.Content.UserVideoStatus.RequestHandlers;

public class GetAllUserVideoStatusRequest : SimplePaginationFilter, IRequest<PaginationResponse<UserVideoStatusDto>>
{
}

public class GetAllUserVideoStatusRequestHandler : IRequestHandler<GetAllUserVideoStatusRequest, PaginationResponse<UserVideoStatusDto>>
{
    private readonly IUserVideoStatusService _userVideoStatusService;

    public GetAllUserVideoStatusRequestHandler(IUserVideoStatusService userVideoStatusService)
    {
        _userVideoStatusService = userVideoStatusService;
    }

    public async Task<PaginationResponse<UserVideoStatusDto>> Handle(GetAllUserVideoStatusRequest request, CancellationToken cancellationToken)
    {
        var paginationRequest = new SimplePaginationFilter()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
        };

        List<UserVideoStatusDto> statuses = await _userVideoStatusService.GetUserVideoStatuses(request, cancellationToken);
        int total = await _userVideoStatusService.CountUserVideoStatuses(cancellationToken);

        return new PaginationResponse<UserVideoStatusDto>(statuses, total, request.PageNumber, request.PageSize);
    }
}
