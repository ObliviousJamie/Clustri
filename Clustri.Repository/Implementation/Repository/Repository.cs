using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Clustri.Repository.Core;
using Neo4jClient;

namespace Clustri.Repository.Implementation.Repository
{
    public class Repository<TNode> : IRepository<TNode> where TNode : class
    {
        private readonly IGraphClient _client;
        private readonly string _type;

        public Repository(IGraphClient client)
        {
            _client = client;
            _type = typeof(TNode).ToString();
        }

        public IEnumerable<TNode> All()
        {
            var query = _client.Cypher
                .Match($"(node:{_type})")
                .Return<TNode>("id(node), node.data");

            return query.Results;
        }

        public virtual void Add(TNode node)
        {
            _client.Cypher
                .Create($"(node:{_type} {{node}})")
                .WithParam("node", node)
                .ExecuteWithoutResults();
        }

        public bool Contains(int id)
        {
            var query = _client.Cypher
                .Match($"(:{_type} {{node}})")
                .Where($"id(n) = {id}")
                .Return<TNode>("node");

            return query.Results.Any();
        }

        public void Remove(int id)
        {
            _client.Cypher
                .Match($"(n:{_type})")
                .Where($"id(n) = {id}")
                .DetachDelete($"n")
                .ExecuteWithoutResults();
        }


    }
}
