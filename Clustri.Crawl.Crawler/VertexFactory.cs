using System.Collections.Generic;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class VertexFactory : IVertexFactory
    {
        public IVertex Create(IProfile profile, IEnumerable<IProfile> edges)
        {
            var vertex = new Vertex(profile.Id, edges);
            return vertex;
        }
    }
}
