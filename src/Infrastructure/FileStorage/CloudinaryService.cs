using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Konet.Application.Common.FileStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Infrastructure.FileStorage;
internal class CloudinaryService : IStorageService
{

    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> settings)
    {
        _cloudinary = new Cloudinary(settings.Value.Url);
    }

    public async Task DeleteAsync(string publicId, ResourceType resourceType = ResourceType.Video)
    {
        if (!string.IsNullOrEmpty(publicId))
        {
            var param = new DeletionParams(publicId) { PublicId = publicId, ResourceType = resourceType };
            var result = await _cloudinary.DestroyAsync(param);
        }
    }

    public async Task<UploadResult> UploadAsync(IFormFile file)
    {
        var fileUploadParams = new VideoUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
        };

        return await _cloudinary.UploadAsync(fileUploadParams).ConfigureAwait(false);
    }
}
