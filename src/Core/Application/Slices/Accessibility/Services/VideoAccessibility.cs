using Konet.Application.Common.Enums;
using Konet.Application.Slices.Accessibility.Interfaces;
using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Accessibility.Services;
public class ContentAccessibility : IContentAccessibility
{
    public VideoPermissionEnum GetVideoPermission(VideoEntity video, DefaultIdType userId, CancellationToken cancellationToken)
    {
        if (video.CreatedBy == userId)
        {
            return VideoPermissionEnum.Owned;
        }

        return VideoPermissionEnum.Viewed;
    }

    public CommentPermissionEnum GetCommentPermisson(VideoCommentsEntity comment, Guid userId, CancellationToken cancellationToken)
    {
        if (comment.CreatedBy == userId)
        {
            return CommentPermissionEnum.Owned;
        }

        return CommentPermissionEnum.Viewed;
    }
}
