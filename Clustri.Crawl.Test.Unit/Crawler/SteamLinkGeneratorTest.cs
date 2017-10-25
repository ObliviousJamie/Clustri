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
            var actual = sut.TransformToFriendLink("johndoe");
            Assert.AreEqual(@"http://steamcommunity.com/id/johndoe/friends/", actual);
        }

        [Test]
        public void TransformToFriendLink_With_Uri_Test()
        {
            var sut = CreateLinkGenerator();
            var uri = new Uri(@"http://steamcommunity.com/id/johndoe");
            var actual = sut.TransformToFriendLink(uri);
            Assert.AreEqual(@"http://steamcommunity.com/id/johndoe/friends/", actual);
        }
        
        [Test]
        public void TransformToFriendLink_With_Uri_With_No_Slash()
        {
            var sut = CreateLinkGenerator();
            var uri = new Uri(@"http://steamcommunity.com/id/johndoe");
            var actual = sut.TransformToFriendLink(uri);
            Assert.AreEqual(@"http://steamcommunity.com/id/johndoe/friends/", actual);
        }

        [Test]
        public void TransformToFriendLink_With_Uri_Profile_Test()
        {
            var sut = CreateLinkGenerator();
            var uri = new Uri(@"http://steamcommunity.com/profiles/johndoe");
            var actual = sut.TransformToFriendLink(uri);
            Assert.AreEqual(@"http://steamcommunity.com/profiles/johndoe/friends/", actual);
        }

        [Test]
        public void TransformToFriendLink_With_Prefix_Id()
        {
            var sut = CreateLinkGenerator();
            var actual = sut.TransformToFriendLink("johndoe", "id");
            Assert.AreEqual(@"http://steamcommunity.com/id/johndoe/friends/", actual);
        }

        [Test]
        public void TransformToFriendLink_With_Prefix_Illegal_Chars()
        {
            var sut = CreateLinkGenerator();
            var actual = sut.TransformToFriendLink("johndoe", " id ");
            Assert.AreEqual(@"http://steamcommunity.com/%20id%20/johndoe/friends/", actual);
        }

        [Test]
        public void TransformLinkTest()
        {
            var linkGen = CreateLinkGenerator();
            var uri = new Uri(@"http://steamcommunity.com/id/johndoe/friends/");
            var actual = linkGen.TransformToUser(uri);
            Assert.AreEqual("johndoe", actual);
        }

        [Test]
        public void TransformLinkUnderscoreTest()
        {
            var linkGen = CreateLinkGenerator();
            var uri = new Uri(@"http://steamcommunity.com/id/john_doe/friends/");
            var actual = linkGen.TransformToUser(uri);
            Assert.AreEqual("john_doe", actual);
        }
    }
}
