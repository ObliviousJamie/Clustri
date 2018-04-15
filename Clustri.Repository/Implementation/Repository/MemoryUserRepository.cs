using System.Collections.Generic;
using Clustri.Repository.Core.Interfaces;
using Clustri.Repository.Entities;

namespace Clustri.Repository.Implementation.Repository
{
    public class MemoryUserRepository : IUserRepository
    {
        private readonly Dictionary<string, User> _dictionary;
        private readonly Dictionary<User, ICollection<User>>_edgeDictionary;


        public MemoryUserRepository()
        {
            _edgeDictionary = new Dictionary<User, ICollection<User>>();
            _dictionary = new Dictionary<string, User>();
        }

        public void Add(User node)
        {
            _dictionary[node.UserId] = node;
            AddKey(node);
        }

        public void Remove(int id)
        {
        }

        public IEnumerable<User> All()
        {
            return _dictionary.Values;
        }

        public bool Contains(int id)
        {
            return false;
        }

        public void DeleteByUser(User user)
        {
            _dictionary.Remove(user.UserId);
        }

        public void RelateFriendsByUser(User user, User friend)
        {
            AddKey(user);
            AddKey(friend);
            _edgeDictionary[user].Add(friend);
            _edgeDictionary[friend].Add(user);
        }

        public IEnumerable<User> GetAllFriends(User user)
        {
            return _edgeDictionary[user];
        }

        public IEnumerable<User> AllUsers()
        {
            return _dictionary.Values;
        }

        private void AddKey(User user)
        {
            if (!_edgeDictionary.ContainsKey(user))
                _edgeDictionary[user] = new HashSet<User>();
        }

    }
}
