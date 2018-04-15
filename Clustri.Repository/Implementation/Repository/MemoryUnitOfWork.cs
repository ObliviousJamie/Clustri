using Clustri.Repository.Core.Interfaces;

namespace Clustri.Repository.Implementation.Repository
{
    class MemoryUnitOfWork : INeo4JUnitOfWork
    {
        public MemoryUnitOfWork()
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
