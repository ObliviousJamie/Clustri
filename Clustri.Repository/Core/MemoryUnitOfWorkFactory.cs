using Clustri.Repository.Core.Interfaces;
using Clustri.Repository.Implementation.Repository;

namespace Clustri.Repository.Core
{
    public class MemoryUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public INeo4JUnitOfWork Create()
        {
            return new MemoryUnitOfWork();
        }
    }
}
