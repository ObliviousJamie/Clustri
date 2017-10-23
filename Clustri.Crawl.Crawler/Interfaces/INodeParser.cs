using System.Collections.Generic;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface INodeParser
    {
        IVertex Parse(string userId);
        IEnumerable<IVertex> ParseFriends(IVertex vertex);
    }
}
