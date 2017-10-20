using System;
using Clustri.Crawl.Crawler.Steam;
using NUnit.Framework;

namespace Clustri.Crawl.Test.Unit.Crawler
{
    public class SteamLinkGeneratorTest
    {
        public SteamLinkGenerator CreateLinkGenerator()
        {
           return new SteamLinkGenerator(); 
        }

        [Test]
        public void TransformUserTest()
        {
            var sut = CreateLinkGenerator();
            var actual = sut.TransformUser("johndoe");
            Assert.AreEqual(@"http://steamcommunity.com/id/johndoe/friends/", actual);
        }

        [Test]
        public void TransformLinkTest()
        {
            var linkGen = CreateLinkGenerator();
            var uri = new Uri(@"http://steamcommunity.com/id/johndoe/friends/");
            var actual = linkGen.TransformLink(uri);
            Assert.AreEqual("johndoe", actual);
        }

        [Test]
        public void TransformLinkUnderscoreTest()
        {
            var linkGen = CreateLinkGenerator();
            var uri = new Uri(@"http://steamcommunity.com/id/john_doe/friends/");
            var actual = linkGen.TransformLink(uri);
            Assert.AreEqual("john_doe", actual);
        }
    }
}
