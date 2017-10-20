using System.Collections.Generic;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IHyperLinkParser
    {
        IEnumerable<string> ParseUser(IProfile profile);
    }
}
