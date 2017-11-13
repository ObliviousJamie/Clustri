using Neo4jClient;
using Neo4jClient.Transactions;

namespace Clustri.Repository.Core.Interfaces
{
    public interface IGraphClientAdapter
    {
        IGraphClient GraphClient { get;  }

        ITransaction Transaction();
        ITransaction BeginTransaction();
        void Dispose();
        bool IsConnected();
        bool InTransaction();

    }
}
