using Konet.Application.Common.FileStorage;
using Konet.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Infrastructure.FileStorage;
public class CloudinarySettings
{
    public static string SectionName => "Cloudinary";
    public string Url { get; set; } = default!;
}
