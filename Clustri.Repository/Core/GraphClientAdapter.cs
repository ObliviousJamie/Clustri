using Clustri.Repository.Core.Interfaces;
using Neo4jClient;
using Neo4jClient.Transactions;

namespace Clustri.Repository.Core
{
    class GraphClientAdapter : IGraphClientAdapter
    {
        private readonly GraphClient _graphClient;

        public IGraphClient GraphClient { get; }

        public GraphClientAdapter(GraphClient graphClient)
        {
            _graphClient = graphClient;
            GraphClient = graphClient;
        }

        public ITransaction Transaction()
        {
            return _graphClient.Transaction;
        }

        public ITransaction BeginTransaction()
        {
            return _graphClient.BeginTransaction();
        }

        public void Dispose()
        {
            _graphClient.Dispose();
        }

        public bool IsConnected()
        {
            return _graphClient.IsConnected;
        }

        public bool InTransaction()
        {
            return _graphClient.InTransaction;
        }
    }
}
