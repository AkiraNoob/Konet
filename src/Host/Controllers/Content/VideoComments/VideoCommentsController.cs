using Konet.Application.Slices.Content.Handlers;
using Konet.Application.Slices.Content.VideoComment.Handlers;
using Konet.Application.Slices.Content.VideoComment.RequestHandlers;
using Konet.Host.Controllers.Content.VideoComments.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Konet.Host.Controllers.Content.VideoComments;

public class VideoCommentsController : VersionedApiController
{
    [HttpPost("")]
    [OpenApiOperation("Create comment.", "")]
    public async Task<ActionResult<string>> CreateCommentAsync(CreatVideoCommentRequest request)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
        ? Unauthorized()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{commentId:guid}")]
    [OpenApiOperation("Delete comment.", "")]
    public async Task<ActionResult<string>> DeleteCommentAsync(Guid commentId)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
        ? Unauthorized()
            : Ok(await Mediator.Send(new DeleteVideoCommentRequest()
            {
                CommentId = commentId
            }));
    }

    [HttpPut("{commentId:guid}")]
    [OpenApiOperation("Update comment.", "")]
    public async Task<ActionResult<string>> UpdateCommentAsync(Guid commentId, FrontEndUpdateCommentRequest request)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
        ? Unauthorized()
            : Ok(await Mediator.Send(new UpdateVideoCommentRequest()
            {
                CommentId = commentId,
                Content = request.Content
            }));
    }
}
