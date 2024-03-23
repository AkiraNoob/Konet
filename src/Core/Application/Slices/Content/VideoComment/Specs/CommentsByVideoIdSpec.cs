using Konet.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.VideoComment.Specs;
public class CommentsByVideoIdSpec : Specification<VideoCommentsEntity>
{
    public CommentsByVideoIdSpec(Guid videoId)
    {
        Query.Where(x => x.VideoId == videoId);
    }
}
