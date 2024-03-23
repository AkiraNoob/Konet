using MassTransit;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konet.Domain.Common.Contracts;
public abstract class BaseEntity : BaseEntity<DefaultIdType>
{
    protected BaseEntity() => Id = Id == default ? NewId.Next().ToGuid() : Id;
}

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public TId Id { get; protected set; } = default!;

    [NotMapped]
    public List<DomainEvent> DomainEvents { get; } = new();
}