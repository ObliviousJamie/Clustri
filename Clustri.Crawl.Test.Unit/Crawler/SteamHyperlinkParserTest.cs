using System;
using System.Linq;
using System.Text.RegularExpressions;
using Clustri.Crawl.Crawler;
using Clustri.Crawl.Crawler.Interfaces;
using Clustri.Crawl.Crawler.Steam;
using Moq;
using NUnit.Framework;

namespace Clustri.Crawl.Test.Unit.Crawler
{
    [TestFixture]
    public class SteamHyperlinkParserTest
    {

        public SteamHyperlinkParser Create_Loader()
        {
            var steamHtml = Resource.ResourceManager.GetString("SteamPage");
            
            //Mock loader
            Mock<IHtmlDownloader> loader = new Mock<IHtmlDownloader>();
            loader.Setup(h => h.Load(It.IsAny<string>())).Returns(steamHtml);

            var parser = new SteamHyperlinkParser(loader.Object);
            return parser;
        }

        public IProfile CreateProfile()
        {
            return new Profile("stagmoor", @"http://steamcommunity.com/id/stagmoor/friends/");
        }

        [Test]
        public void ReturnsNonEmptyList()
        {
            var sut = Create_Loader();
            var profile = CreateProfile();
            var enumerable = sut.ParseUser(profile);
            var actual = enumerable.ToList();
            Assert.NotZero(actual.Count);
        }

        [Test]
        public void ReturnsUris()
        {
            var sut = Create_Loader();
            var profile = CreateProfile();
            var enumerable = sut.ParseUser(profile);
            var list = enumerable.ToList();
            var isUri = false;
            foreach (var link in list)
            {
                isUri = Uri.IsWellFormedUriString(link, UriKind.Absolute);
                if (!isUri)
                    break;
            }
            Assert.IsTrue(isUri);
        }


        [Test]
        public void MatchesRegex()
        {
            var sut = Create_Loader();
            var profile = CreateProfile();
            var enumerable = sut.ParseUser(profile);
            var list = enumerable.ToList();

            string pattern = @"http:\/\/steamcommunity\.com\/(id|profiles)/\w+";
            var isMatch = false;

            foreach (var link in list)
            {
                isMatch = Regex.IsMatch(link, pattern);
                if (!isMatch)
                    break;
            }
            Assert.IsTrue(isMatch);
        }
    }
}
