namespace Clustri.Repository.Core.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        INeo4JUnitOfWork Create();
    }
}