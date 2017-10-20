using System;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler.Steam
{
    public class SteamProfileFactory : IProfileFactory
    {
        private readonly ILinkGenerator _linkGenerator;

        public SteamProfileFactory(ILinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }


        public IProfile Create(string name)
        {
            var link = _linkGenerator.TransformUser(name);
            return new Profile(name, link);
        }

        public IProfile Create(string name, string link)
        {
            return new Profile(name, link);
        }

        public IProfile Create(Uri link)
        {
            var name = _linkGenerator.TransformLink(link);
            return new Profile(name, link.AbsoluteUri);
        }
    }
}
