using System;

namespace Clustri.Repository.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();
    }
}
