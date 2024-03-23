using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Common.FileStorage;
public interface IStorageService : IScopedService
{
    Task<UploadResult> UploadAsync(IFormFile file);
    Task DeleteAsync(string fileId, ResourceType resourceType);
}
