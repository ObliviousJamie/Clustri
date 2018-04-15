using System;
using System.Collections.Generic;
using System.IO;
using Clustri.Crawl.Console.Interfaces;
using Clustri.Repository.Core.Interfaces;
using Clustri.Repository.Entities;
using Neo4jClient.Gremlin;

namespace Clustri.Crawl.Console
{
    public class Output : IOutput
    {
        private readonly IUserRepository _userRepo;
        private readonly string _absolutePath;

        public Output(IUserRepository userRepo, string absolutePath)
        {
            _userRepo = userRepo;
            _absolutePath = absolutePath;
        }


        public void Write()
        {
            var users = _userRepo.AllUsers();
            foreach (var user in users)
            {
                var edges = _userRepo.GetAllFriends(user);

                WriteEdge(user, edges);

                if (user.Community)
                {
                    WriteSeed(user);
                }
            }
        }

        private void WriteEdge(User user, IEnumerable<User> edges)
        {
            foreach (var edge in edges)
            {
                var text = $"{user.UserId} {edge.UserId}" + Environment.NewLine;
                File.AppendAllText($"{_absolutePath}/steam_edgelist", text);
            }
        }

        private void WriteSeed(User user)
        {
                var text = $"{user.UserId} ";
                File.AppendAllText($"{_absolutePath}/steam_seed_list.txt", text);
        }
    }
}
