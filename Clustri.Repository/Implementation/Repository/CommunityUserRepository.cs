using Clustri.Repository.Entities;
using Neo4jClient;

namespace Clustri.Repository.Implementation.Repository
{
    public class CommunityUserRepository : UserRepository
    {
        public CommunityUserRepository(IGraphClient client) : base(client)
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
                    .Set("user.community = {community}")
                    .WithParams(new
                    {
                        userId = user.UserId,
                        weight = user.Weight,
                        community = user.Community
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
                        community = user.Community
                    })
                    .ExecuteWithoutResults();
            }
        }
    }
}
