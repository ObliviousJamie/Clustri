using System.Collections.Generic;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IScheduler : IEnumerable<IVertex>
    {
        void Add(IVertex vertex);
        void Add(IEnumerable<IVertex> vertices);
    }
}
