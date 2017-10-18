namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IFilterFactory<T>
    {
        IFilter<T> Create();
    }
}
