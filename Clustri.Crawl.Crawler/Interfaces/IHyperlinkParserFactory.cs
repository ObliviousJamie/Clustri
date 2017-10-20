namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IHyperlinkParserFactory
    {
        IHyperLinkParser Create(Domain domain);
    }
}
