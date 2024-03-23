using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.UserVideoStatus.Dtos;
public class UserVideoStatusDto
{
    public Guid VideoId { get; set; }
    public string OwnerId { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string Title { get; set; } = default!;
    public bool IsDelete { get; set; }
    public string? Thumbnail { get; set; }
}
