using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.VideoComment.Specs;
public class VideoByCommentIdSpec : Specification<VideoCommentsEntity>
{
    public VideoByCommentIdSpec(Guid commentId)
    {
        Query.Where(x => x.Id == commentId).Include(x => x.Video);
    }
}
