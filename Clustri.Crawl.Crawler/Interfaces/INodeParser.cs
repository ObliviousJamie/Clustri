namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface INodeParser
    {
        IVertex ParseFriends(string userId);
    }
}
