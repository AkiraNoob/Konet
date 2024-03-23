using Konet.Application.Common.Enums;
using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Accessibility.Interfaces;
public interface IContentAccessibility : ITransientService
{
    public VideoPermissionEnum GetVideoPermission(VideoEntity video, Guid userId, CancellationToken cancellationToken);
    public CommentPermissionEnum GetCommentPermisson(VideoCommentsEntity comment, Guid userId, CancellationToken cancellationToken);
}
