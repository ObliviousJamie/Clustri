namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IFilter<T>
    {
        void Filter(T input, out T output);
    }
}
