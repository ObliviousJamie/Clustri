using System.Threading.Tasks;
using AngleSharp;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    internal class HtmlLoader : IHtmlDownloader
    {

        public string Load(string address)
        {
            Task<string> page = RequestPage(address);
            return page.Result;
        }

        private async Task<string> RequestPage(string address)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(address);
            return document.Body.OuterHtml;
        }

    }
}
