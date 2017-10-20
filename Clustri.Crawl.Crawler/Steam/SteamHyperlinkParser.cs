using System.Collections.Generic;
using System.Linq;
using AngleSharp.Parser.Html;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler.Steam
{
    public class SteamHyperlinkParser : IHyperLinkParser
    {
        private readonly IHtmlDownloader _htmlDownloader;

        public SteamHyperlinkParser(IHtmlDownloader htmlDownloader)
        {
            _htmlDownloader = htmlDownloader;
        }

        public IEnumerable<string> ParseUser(IProfile profile)
        {
            //Load page
            var pageHtml = _htmlDownloader.Load(profile.Link);

            //Parse
            var parser = new HtmlParser();
            var document = parser.Parse(pageHtml);
            var links = from documentLink in document.Links
                        let className = documentLink.ClassName
                        where className == "friendBlockLinkOverlay"
                        select documentLink.Attributes["href"].Value;

            return links;
        }
    }
}
