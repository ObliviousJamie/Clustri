using System;
using System.Collections.Generic;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class GeneralCrawler : IWebCrawler
    {
        private readonly IScheduler _scheduler;
        private readonly INodeParser _parser;

        public GeneralCrawler(IScheduler scheduler, INodeParser parser )
        {
            _scheduler = scheduler;
            _parser = parser;
        }


        public IEnumerable<IVertex> Crawl(Domain domain, string userId)
        {
            //Parse first page
            var exploredVertex = _parser.Parse(userId);
            var rootFriends = _parser.ParseFriends(exploredVertex);
            _scheduler.Add(rootFriends);

            yield return exploredVertex;
            //Next scheduled pages
            foreach (var unexploredVertex in _scheduler)
            {
                var friends = _parser.ParseFriends(unexploredVertex);
                _scheduler.Add(friends);
                yield return unexploredVertex;
            }
            throw new Exception($"Domain {domain} not recognised");
        }
    }
}
