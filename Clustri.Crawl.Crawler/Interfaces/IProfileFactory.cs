using System;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IProfileFactory
    {
        IProfile Create(Uri link);
        IProfile Create(string name, string link);
    }
}
