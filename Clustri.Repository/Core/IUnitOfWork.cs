using System;

namespace Clustri.Repository.Core
{
    interface IUnitOfWork : IDisposable
    {
        bool Complete();
    }
}
