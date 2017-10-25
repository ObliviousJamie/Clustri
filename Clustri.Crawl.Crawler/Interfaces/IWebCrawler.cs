using System;
using System.Collections.Generic;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IWebCrawler
    {
        IEnumerable<IVertex> Crawl(Uri user);
    }
}
