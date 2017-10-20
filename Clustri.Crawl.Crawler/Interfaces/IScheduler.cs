using System.Collections.Generic;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IScheduler : IEnumerable<IProfile>
    {
        void Add(IProfile profile);
        void Add(IEnumerable<IProfile> profiles);
    }
}
