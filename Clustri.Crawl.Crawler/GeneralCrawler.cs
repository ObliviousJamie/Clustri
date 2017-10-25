using System;
using System.Collections.Generic;
using System.Linq;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class GeneralCrawler : IWebCrawler
    {
        private readonly IScheduler _scheduler;
        private readonly INodeParser _parser;

        public GeneralCrawler(IScheduler scheduler, INodeParser parser)
        {
            _scheduler = scheduler;
            _parser = parser;
        }


        public IEnumerable<IVertex> Crawl(Uri link)
        {
            //Parse first page
            var exploredVertex = _parser.Parse(link);
            _scheduler.Add(exploredVertex);

            ////Next scheduled pages
            while (_scheduler.Any())
            {
                foreach (var unexploredVertex in _scheduler)
                {
                    var friends = _parser.ParseFriends(unexploredVertex);
                    _scheduler.Add(friends);
                    _scheduler.Remove(unexploredVertex);
                    yield return unexploredVertex;
                }
            }
        }
    }
}
