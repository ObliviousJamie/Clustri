using System.Collections.Generic;
using Clustri.Repository.Core.Interfaces;
using Clustri.Repository.Entities;
using Neo4jClient;

namespace Clustri.Repository.Implementation.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IGraphClient client) : base(client)
        {
        }

        public override void Add(User user)
        {
            //Overwrite existing data or update
            if (user.Weight > 0)
            {
                Client.Cypher
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
                Client.Cypher
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
            Client.Cypher
                .Match("(user1:User)")
                .Where((User user1) => user1.UserId == user.UserId)
                .DetachDelete("user1")
                .ExecuteWithoutResults();
        }

        public void RelateFriendsByUser(User user, User friend)
        {
            Client.Cypher
                .Match("(user1:User)", "(user2:User)")
                .Where((User user1) => user1.UserId == user.UserId)
                .AndWhere((User user2) => user2.UserId == friend.UserId)
                .CreateUnique("(user1)-[:FRIENDS_WITH]-(user2)")
                .ExecuteWithoutResults();
        }

        public IEnumerable<User> GetAllFriends(User user)
        {
            var query = Client.Cypher
                .Match("(user:User)-[r:FRIENDS_WITH]-(friend:User)")
                .Where($"user.userId = \"{user.UserId}\"")
                .Return<User>("friend");

            return query.Results;
        }

        public IEnumerable<User> AllUsers()
        {
            var query = Client.Cypher
                .Match($"(user:User)")
                .Where($"user.userId <> \"null\"")
                .Return<User>("user");

            return query.Results;
        }

        public IEnumerable<User> AllSeeds()
        {
            var query = Client.Cypher
                .Match($"(node:User)")
                .Where($"node.community = true")
                .Return<User>("node");

            return query.Results;
        }

    }
}
