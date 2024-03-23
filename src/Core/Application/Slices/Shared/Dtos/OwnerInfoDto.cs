using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Shared.Dtos;
public class OwnerInfoDto
{
    public string Id { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string? Avatar { get; set; }
}
