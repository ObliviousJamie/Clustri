namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IFilterFactory<T>
    {
        ILogSet<T> Create();
    }
}
