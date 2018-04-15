using System;
using System.Collections.Generic;
using System.Text;
using Clustri.Repository.Core.Interfaces;

namespace Clustri.Repository.Implementation.Repository
{
    class FakeUnitOfWork : INeo4JUnitOfWork
    {
        public FakeUnitOfWork()
        {
            Users = new MemoryUserRepository();
        }
        public IUserRepository Users { get; }

        public void Dispose()
        {
        }

        public void Complete()
        {
        }

        public void Start()
        {
        }
    }
}
