using System.Collections.Generic;
using Clustri.Repository.Core;
using Clustri.Repository.Entities;
using Neo4jClient;

namespace Clustri.Repository.Implementation.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IGraphClient _client;

        public UserRepository(IGraphClient client) : base(client)
        {
            _client = client;
        }

        public override void Add(User user)
        {
            //Overwrite existing data or update
            if (user.Weight > 0)
            {
                _client.Cypher
                    .Merge("(user:User { userId: {userId} })")
                    .Set("user.weight = {weight}")
                    .WithParams(new
                    {
                        userId = user.UserId,
                        weight = user.Weight,
                    })
                    .ExecuteWithoutResults();
            }
            else
            {
                _client.Cypher
                    .Merge("(user:User { userId: {userId} })")
                    .OnCreate()
                    .Set("user.userId = {userId}")
                    .WithParams(new
                    {
                        userId = user.UserId,
                    })
                    .ExecuteWithoutResults();
            }
        }


        public void DeleteByUser(User user)
        {
            _client.Cypher
                .Match("(user1:User)")
                .Where((User user1) => user1.UserId == user.UserId)
                .DetachDelete("user1")
                .ExecuteWithoutResults();
        }

        public void RelateFriendsByUser(User user, User friend)
        {
            _client.Cypher
                .Match("(user1:User)", "(user2:User)")
                .Where((User user1) => user1.UserId == user.UserId)
                .AndWhere((User user2) => user2.UserId == friend.UserId)
                .CreateUnique("(user1)-[:FRIENDS_WITH]-(user2)")
                .ExecuteWithoutResults();
        }

        public IEnumerable<User> GetAllFriends(User user)
        {
            var query = _client.Cypher
                .Match("(user:User)-[r:FRIENDS_WITH]-(friend:User)")
                .Where($"user.UserId = \"{user.UserId}\"")
                .Return<User>("friend");

            return query.Results;
        }
    }
}
