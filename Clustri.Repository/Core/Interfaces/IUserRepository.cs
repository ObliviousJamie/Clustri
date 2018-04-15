using System.Collections.Generic;
using Clustri.Repository.Entities;

namespace Clustri.Repository.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        void DeleteByUser(User user);

        void RelateFriendsByUser(User user, User friend);

        IEnumerable<User> GetAllFriends(User user);

        IEnumerable<User> AllUsers();

        IEnumerable<User> AllSeeds();
    }
}
