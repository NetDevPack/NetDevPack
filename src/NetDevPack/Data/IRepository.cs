using System;
using NetDevPack.Domain;

namespace NetDevPack.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}