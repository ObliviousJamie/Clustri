namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IProfileFactory
    {
        IProfile Create(string name);
        IProfile Create(string name, string link);
    }
}
