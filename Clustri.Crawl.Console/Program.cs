using System;
using Clustri.Repository.Core;
using Clustri.Repository.Entities;

namespace Clustri.Crawl.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioc = new IocContainer();
            var crawler = ioc.SteamCrawler(1000, 200);

            var whitespace = new String(' ', 5);

            var link = new Uri(@"http://steamcommunity.com/id/faucomte97");

            var unitOfWork = new UnitOfWorkFactory("http://localhost:7474/db/data", args[0], args[1]).Create();
            var userRepo = unitOfWork.Users;

            foreach (var vertex in crawler.Crawl(link))
            {
                System.Console.WriteLine(vertex.Id);
                System.Console.WriteLine(vertex.Weight);
                System.Console.WriteLine("---Links---");

                var newUser = new User
                {
                    UserId = vertex.Id,
                    Weight = vertex.Weight
                };

                userRepo.Add(newUser);


                foreach (var profile in vertex.Degrees)
                {
                    System.Console.WriteLine($"{whitespace}{profile.Id}");

                    var friend = new User
                    {
                        UserId = profile.Id,
                    };

                    userRepo.Add(friend);
                    userRepo.RelateFriendsByUser(newUser, friend);
                }

            }
        }
    }
}
