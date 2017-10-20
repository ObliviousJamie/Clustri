using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class Profile : IProfile
    {
        public string Id { get; }
        public string Link { get; }

        public Profile(string id, string link)
        {
            Id = id;
            Link = link;
        }
    }
}
