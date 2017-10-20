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
            var exploredVertex = _parser.ParseFriends(userId);
            _scheduler.Add(exploredVertex.Degrees);

            yield return exploredVertex;
            //Next scheduled pages
            foreach (var unexploredVertex in _scheduler)
            {
                var explored = _parser.ParseFriends(unexploredVertex.Id);
                _scheduler.Add(explored.Degrees);
                yield return explored;
            }
            throw new Exception($"Domain {domain} not recognised");
        }



    }
}
