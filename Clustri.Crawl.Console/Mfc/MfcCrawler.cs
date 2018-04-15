using System;
using System.Collections.Generic;
using Clustri.Crawl.Console.Interfaces;
using Clustri.Crawl.Crawler.Interfaces;
using Clustri.Repository.Core.Interfaces;
using Clustri.Repository.Entities;

namespace Clustri.Crawl.Console.Mfc
{
    public class MfcCrawler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPrinter _printer;
        private readonly ICommunityDecider _communityDecider;
        private readonly IWebCrawler crawler;

        public MfcCrawler(IocContainer ioc, IUserRepository userRepository, IPrinter printer, 
            ICommunityDecider communityDecider, int waitTime = 200)
        {
            _userRepository = userRepository;
            _printer = printer;
            _communityDecider = communityDecider;
            crawler = ioc.SteamCrawler(1000, waitTime);
        }


        public void Crawl(int maxDepth, Uri link)
        {
            var depth = 0;
            var lastWeight = 0.5;

            foreach (var vertex in crawler.Crawl(link))
            {
                var community = false;
                _printer.Print(vertex);

                var shouldCreate = _communityDecider.ShouldCreateCommunity(lastWeight, vertex.Weight);
                lastWeight = vertex.Weight;

                if (shouldCreate)
                    community = true;

                var newUser = new User { UserId = vertex.Id, Weight = vertex.Weight, Community = community};

                _userRepository.Add(newUser);

                depth++;
                if (depth >= maxDepth)
                    break;

                AddChildren(newUser, vertex.Degrees);
            }
        }


        private void AddChildren(User parent, IEnumerable<IProfile> children)
        {
            foreach (var profile in children)
            {
                _printer.Print(profile, true);

                var friend = new User { UserId = profile.Id, Community = parent.Community};

                _userRepository.Add(friend);
                _userRepository.RelateFriendsByUser(parent, friend);
            }
        }

    }
}
