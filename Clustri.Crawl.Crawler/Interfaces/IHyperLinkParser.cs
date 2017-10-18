using System;
using System.Collections.Generic;
using System.Text;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IHyperLinkParser
    {
        Tuple<IExploredVertex, IEnumerable<IUnexploredVertex>> ParseFriends(string userId);
    }
}
