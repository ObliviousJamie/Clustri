using System;
using System.Collections.Generic;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class GeneralCrawler : IWebCrawler
    {
        private readonly IScheduler _scheduler;
        private readonly IHyperlinkParserFactory _hyperlinkParserFactory;

        public GeneralCrawler(IScheduler scheduler, IHyperlinkParserFactory hyperlinkParserFactory)
        {
            _scheduler = scheduler;
            _hyperlinkParserFactory = hyperlinkParserFactory;
        }


        public IEnumerable<IExploredVertex> Crawl(Domain domain, string userId)
        {
            var hyperlinkParser = _hyperlinkParserFactory.Create(domain); 
            //Parse first page
            var exploredTuple = ParsePage(userId, hyperlinkParser);
            _scheduler.Add(exploredTuple);

            yield return exploredTuple.Item1;
            //Next scheduled pages
            foreach (var unexploredVertex in _scheduler)
            {
                var newFriends = ParsePage(unexploredVertex.Name, hyperlinkParser);
                _scheduler.Add(newFriends);
                yield return newFriends.Item1;
            }
            throw new Exception($"Domain {domain} not recognised");
        }

        private Tuple<IExploredVertex, IEnumerable<IUnexploredVertex>> ParsePage(string userId, IHyperLinkParser parser)
        {
            Tuple<IExploredVertex, IEnumerable<IUnexploredVertex>> exploredTuple = parser.ParseFriends(userId);
            return exploredTuple;
        }


    }
}
