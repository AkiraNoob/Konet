using CloudinaryDotNet.Actions;
using Konet.Domain.Content;

namespace Konet.Application.Slices.Content.UserVideoStatus.Specs;
public class UserVideoStatusByUserIdAndVideoIdSpec : Specification<UserVideoStatusEntity>
{
    public UserVideoStatusByUserIdAndVideoIdSpec(string userId, Guid videoId)
    {
        Query.Where(x => x.UserId == userId && x.VideoId == videoId);
    }
}
