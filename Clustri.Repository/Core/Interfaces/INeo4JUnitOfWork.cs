namespace Clustri.Repository.Core.Interfaces
{
    public interface INeo4JUnitOfWork : IUnitOfWork
    {
        IUserRepository Users { get;  }
        void Start();
    }
}
