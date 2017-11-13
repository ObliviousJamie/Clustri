using System;
using Clustri.Repository.Core.Interfaces;
using Clustri.Repository.Implementation.Repository;

namespace Clustri.Repository.Implementation
{
    public class UnitOfWork : INeo4JUnitOfWork
    {
        private readonly IGraphClientAdapter _graphClientAdapter;

        public IUserRepository Users { get; }

        public UnitOfWork(IGraphClientAdapter graphClientAdapter)
        {
            _graphClientAdapter = graphClientAdapter;
            Users = new UserRepository(_graphClientAdapter.GraphClient);
        }

        public void Dispose()
        {
            if(_graphClientAdapter.InTransaction())
                _graphClientAdapter.Transaction().Dispose();

            _graphClientAdapter.Dispose();
        }


        public void Complete()
        {
            if (!_graphClientAdapter.IsConnected())
                throw new Exception("Graph client is not connected");

            //Check if transaction exists and is open
            var transactionOpen = _graphClientAdapter.Transaction()?.IsOpen ?? false;

            if (transactionOpen)
                _graphClientAdapter.Transaction().Commit();
            else
                throw new Exception("Transation not open");
        }

        public void Start()
        {
            if (!_graphClientAdapter.IsConnected())
                throw new Exception("Graph client is not connected");
            if(_graphClientAdapter.InTransaction())
                throw new Exception("Graph client transaction already exists");

            _graphClientAdapter.BeginTransaction();
        }
    }
}
