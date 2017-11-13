using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Clustri.Crawl.Crawler;
using Clustri.Crawl.Crawler.Interfaces;
using Clustri.Crawl.Crawler.Steam;
using SimpleInjector;

namespace Clustri.Crawl.Console
{
    public class IocContainer
    {
        public IWebCrawler SteamCrawler(int cacheSize, int waitTime)
        {
            var container = new Container();
            container.Register<IWebCrawler, GeneralCrawler>();
            container.Register<INodeParser, NodeParser>();
            container.Register<IScheduler, MfcScheduler>();
            //container.Register<IScheduler>((() => new BfsScheduler(new VisitSet<IVertex>(), new Queue<IVertex>())));
            container.Register<IHyperLinkParser, SteamHyperlinkParser>();
            container.Register<IProfileFactory, SteamProfileFactory>();
            container.Register<IVertexCache>(() => new VertexCache(cacheSize));
            container.Register<IDictionary<IVertex, double>>(() => new Dictionary<IVertex, double>());
            container.Register<ILogSet<IVertex>, VisitSet<IVertex>>();
            container.Register<ILinkGenerator, SteamLinkGenerator>();
            container.Register<IVertexFactory, VertexFactory>();
            container.Register<IHtmlDownloader, HtmlLoader>();
            container.Register<IPause>(() => new RequestPause(waitTime));

            container.Verify();
            return container.GetInstance<IWebCrawler>();
        }
    }
}
