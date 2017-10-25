using System;
using System.Text.RegularExpressions;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler.Steam
{
    public class SteamLinkGenerator : ILinkGenerator
    {
        public string TransformToFriendLink(string user, string prefix = null)
        {
            if (String.IsNullOrEmpty(prefix))
                prefix = "id";
            if(string.IsNullOrEmpty(user))
                throw new Exception("User does not exist exception");

            var steamLink = @"http://steamcommunity.com";
            var friends = @"friends";
            var link = $"{steamLink}/{prefix}/{user}/{friends}/";
            Uri realUri;

            var successfulUri = Uri.TryCreate(link, UriKind.Absolute, out realUri);
            if(!successfulUri)
                throw new Exception("Could not create uri");

            return realUri.AbsoluteUri;
        }

        public string TransformToFriendLink(Uri profileLink)
        {
            var friends = @"/friends/";
            var link = profileLink.AbsoluteUri;
            return $"{link}{friends}";
        }

        public string TransformToUser(Uri link)
        {
            var pattern = @"http[s]?:\/\/steamcommunity\.com\/(profiles|id)\/(\w*)";
            var matches = Regex.Matches(link.AbsoluteUri, pattern, RegexOptions.Singleline);
            return matches[0].Groups[2].Value;
        }
    }
}
