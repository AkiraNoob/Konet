using Finbuckle.MultiTenant;
using Konet.Application.Common.Events;
using Konet.Application.Common.Interfaces;
using Konet.Domain.Content;
using Konet.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Konet.Infrastructure.Persistence.Context;
public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<VideoEntity> Video => Set<VideoEntity>();
    public DbSet<UserVideoStatusEntity> UserVideoStatus => Set<UserVideoStatusEntity>();
    public DbSet<VideoCommentsEntity> VideoComments => Set<VideoCommentsEntity>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Konet);
    }
}