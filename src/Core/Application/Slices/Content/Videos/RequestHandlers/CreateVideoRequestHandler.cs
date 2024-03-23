using Konet.Domain.Content;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konet.Application.Slices.Content.Handlers;
public class CreateVideoRequest : IRequest<DefaultIdType>
{
    public string Title { get; set; } = default!;
    public IFormFile File { get; set; } = default!;
}

public class CreateVideoRequestHandler : IRequestHandler<CreateVideoRequest, DefaultIdType>
{
    private readonly ICurrentUser _currentUser;
    private readonly IRepository<VideoEntity> _repository;
    private readonly IStorageService _storageService;

    public CreateVideoRequestHandler(IRepository<VideoEntity> repository, ICurrentUser currentUser, IStorageService storageService)
    {
        _repository = repository;
        _currentUser = currentUser;
        _storageService = storageService;
    }

    public async Task<DefaultIdType> Handle(CreateVideoRequest request, CancellationToken cancellationToken)
    {
        string userId = _currentUser.GetUserId().ToString();

        var uploadResult = await _storageService.UploadAsync(request.File);

        var video = new VideoEntity(uploadResult.Url.ToString(), request.Title, userId, uploadResult.PublicId);

        await _repository.AddAsync(video, cancellationToken);

        return video.Id;
    }
}