using System;
using System.Text.RegularExpressions;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler.Steam
{
    public class SteamLinkGenerator : ILinkGenerator
    {
        public string TransformUser(string user)
        {
            var steamLink = @"http://steamcommunity.com/id/";
            var friends = @"/friends/";
            return $"{steamLink}{user}{friends}";
        }

        public string TransformLink(Uri link)
        {
            var pattern = @"http[s]?:\/\/steamcommunity\.com\/(profiles|id)\/(\w*)";
            var matches = Regex.Matches(link.AbsoluteUri, pattern, RegexOptions.Singleline);
            return matches[0].Groups[2].Value;
        }
    }
}
