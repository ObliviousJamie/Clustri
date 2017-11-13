using System.Collections.Generic;
using System.Linq;
using Clustri.Repository.Core.Interfaces;
using Neo4jClient;

namespace Clustri.Repository.Implementation.Repository
{
    public class Repository<TNode> : IRepository<TNode> where TNode : class
    {
        protected readonly IGraphClient Client;
        private readonly string _type;

        public Repository(IGraphClient client)
        {
            Client = client;
            _type = typeof(TNode).ToString();
        }

        public IEnumerable<TNode> All()
        {
            var query = Client.Cypher
                .Match($"(node:{_type})")
                .Return<TNode>("id(node), node.data");

            return query.Results;
        }

        public virtual void Add(TNode node)
        {
            Client.Cypher
                .Create($"(node:{_type} {{node}})")
                .WithParam("node", node)
                .ExecuteWithoutResults();
        }

        public bool Contains(int id)
        {
            var query = Client.Cypher
                .Match($"(:{_type} {{node}})")
                .Where($"id(n) = {id}")
                .Return<TNode>("node");

            return query.Results.Any();
        }

        public void Remove(int id)
        {
            Client.Cypher
                .Match($"(n:{_type})")
                .Where($"id(n) = {id}")
                .DetachDelete($"n")
                .ExecuteWithoutResults();
        }


    }
}
