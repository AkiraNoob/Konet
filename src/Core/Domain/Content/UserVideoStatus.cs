using Konet.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Domain.Content;
public class UserVideoStatusEntity : AuditableEntity, IAggregateRoot
{
    public string UserId { get; set; }
    public DefaultIdType VideoId { get; set; }
    public UserVideoStatusEnum Status { get; set; }
    public VideoEntity Video { get; private set; } = default!;
    public UserVideoStatusEntity(string userId, DefaultIdType videoId, UserVideoStatusEnum status = UserVideoStatusEnum.Watched)
    {
        UserId = userId;
        VideoId = videoId;
        Status = status;
    }
}
