using Hangfire;
using Konet.Application.Slices.Content.UserVideoStatus.Dtos;
using Konet.Application.Slices.Content.UserVideoStatus.RequestHandlers;
using Konet.Application.Slices.Content.VideoComment.Handlers;
using Konet.Host.Controllers.Content.UserVideoStatus.Request;
using System.Security.Claims;

namespace Konet.Host.Controllers.Content.UserVideoStatus;

public class UserVideoStatusController : VersionedApiController
{
    [HttpGet("")]
    [OpenApiOperation("Get user video status.", "")]
    public async Task<ActionResult<PaginationResponse<UserVideoStatusDto>>> GetAllUserVideoStatusAsync(GetAllUserVideoStatusRequest request)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
        ? Unauthorized()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("{videoId:guid}")]
    [OpenApiOperation("Update user status of a video.", "")]
    public async Task<ActionResult<string>> UpdateUserVideoStatusAsync(Guid videoId, [FromBody] FrontEndUpdateUserVideoStatus request)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
        ? Unauthorized()
            : Ok(await Mediator.Send(new UpdateUserVideoStatusRequest()
            {
                Status = Enum.Parse<UserVideoStatusEnum>(request.Status),
                VideoId = videoId
            }));
    }

    [HttpDelete("{videoId:guid}")]
    [OpenApiOperation("Delete user status of a video.", "")]
    public async Task<ActionResult<string>> DeleteUserVideoStatusAsync(Guid videoId)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
        ? Unauthorized()
            : Ok(await Mediator.Send(new DeleteUserVideoStatusRequest()
            {
                VideoId = videoId
            }));
    }
}
