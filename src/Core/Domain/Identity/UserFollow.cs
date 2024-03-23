using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Domain.Identity;
public class UserFollow : AuditableEntity, IAggregateRoot
{
    public string FollowerId { get; set; }
    public string TargetId { get; set; }

    public UserFollow(string followerId, string targetId)
    {
        this.FollowerId = followerId;
        this.TargetId = targetId;
    }
}
