using System;
using System.Collections.Generic;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface INodeParser
    {
        IVertex Parse(Uri userHomePageLink);
        IEnumerable<IVertex> ParseFriends(IVertex vertex);
    }
}
