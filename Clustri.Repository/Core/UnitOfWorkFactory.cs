using System;
using Clustri.Repository.Core.Interfaces;
using Clustri.Repository.Implementation;
using Neo4jClient;

namespace Clustri.Repository.Core
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly GraphClient _client;

        public UnitOfWorkFactory(string address)
        {
            _client = new GraphClient(new Uri(address));
        }

        public UnitOfWorkFactory(string address, string user, string password)
        {
            _client = new GraphClient(new Uri(address), user, password);
        }

        public INeo4JUnitOfWork Create()
        {
            _client.Connect();
            var adapter = new GraphClientAdapter(_client);
            return new UnitOfWork(adapter);
        }
    }
}
