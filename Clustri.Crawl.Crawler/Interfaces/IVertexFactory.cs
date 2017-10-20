using System.Collections.Generic;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IVertexFactory
    {
        IVertex Create(IProfile profile, IEnumerable<IProfile> edges);
    }
}
