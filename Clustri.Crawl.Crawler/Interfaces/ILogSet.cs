namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface ILogSet<T>
    {
        bool Exists(T input);
        void Add(T item);
    }
}
