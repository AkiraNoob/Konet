using Hangfire;
using Konet.Application.Identity.Users;
using Konet.Application.Slices.Content;
using Konet.Application.Slices.Content.Dtos;
using Konet.Application.Slices.Content.Handlers;
using Konet.Host.Controllers.Content.Videos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Konet.Host.Controllers.Content.Videos;
public class VideoController : VersionedApiController
{
    [HttpPost("")]
    [OpenApiOperation("Create video.", "")]
    public async Task<ActionResult<string>> CreateVideoAsync([FromForm] CreateVideoRequest request)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
        ? Unauthorized()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("soft/{videoId:Guid}")]
    [OpenApiOperation("Soft delete video.", "")]
    public async Task<ActionResult<string>> SoftDeleteVideoAsync(Guid videoId)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
        ? Unauthorized()
            : Ok(await Mediator.Send(new SoftDeleteVideoRequest()
            {
                VideoId = videoId,
            }));
    }

    [HttpDelete("hard/{videoId:Guid}")]
    [OpenApiOperation("Hard delete video.", "")]
    public async Task<ActionResult<string>> HardDeleteVideoAsync(Guid videoId)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
        ? Unauthorized()
            : Ok(await Mediator.Send(new HardDeleteVideoRequest()
            {
                VideoId = videoId
            }));
    }

    [HttpGet("")]
    [AllowAnonymous]
    [OpenApiOperation("Get all videos.", "")]
    public async Task<ActionResult<PaginationResponse<VideoWithOwnerInfoDto>>> GetAllVideoAsync([FromQuery] GetAllVideosRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

    [HttpPut("{videoId:guid}")]
    public async Task<ActionResult<string>> UpdateVideoByIdAsync(DefaultIdType videoId, FrontEndUpdateVideoRequest request)
    {
        return Ok(await Mediator.Send(new UpdateVideoRequest()
        {
            Title = request.Title,
            VideoId = videoId,
        }));
    }
}
