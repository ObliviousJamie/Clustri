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

        public IProfile Create(string name, string link)
        {
            return new Profile(name, link);
        }

        public IProfile Create(Uri incompleteUri)
        {
            var friendLink = _linkGenerator.TransformToFriendLink(incompleteUri);
            var name = _linkGenerator.TransformToUser(new Uri(friendLink));
            return Create(name, friendLink);
        }
    }
}
