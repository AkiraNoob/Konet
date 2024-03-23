using Konet.Application.Slices.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.Dtos;
public class VideoDto
{
    public DefaultIdType Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
    public string? Thumbnail { get; set; }
    public UserVideoStatusEnum? VideoStatus { get; set; }
}

public class VideoWithOwnerInfoDto : VideoDto
{
    public OwnerInfoDto OwnerVideo { get; set; } = default!;
}
