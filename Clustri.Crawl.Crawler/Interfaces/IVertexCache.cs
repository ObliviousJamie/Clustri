namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IVertexCache
    {
        void Save(IVertex item);
        IVertex Retrieve(string id);
    }
}
