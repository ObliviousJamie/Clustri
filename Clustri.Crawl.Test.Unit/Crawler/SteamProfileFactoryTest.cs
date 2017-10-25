using System;
using Clustri.Crawl.Crawler.Interfaces;
using Clustri.Crawl.Crawler.Steam;
using Moq;
using NUnit.Framework;

namespace Clustri.Crawl.Test.Unit.Crawler
{
    public class SteamProfileFactoryTest
    {
        public SteamProfileFactory Create_SteamProfileFactory()
        {
            var mockLinkGenerator = new Mock<ILinkGenerator>();
            mockLinkGenerator.Setup(l => l.TransformToFriendLink(It.IsAny<string>(), It.IsAny<string>())).
                Returns(@"http://steamcommunity.com/id/johndoe/friends/");

            mockLinkGenerator.Setup(l => l.TransformToFriendLink(It.IsAny<Uri>())).
                Returns(@"http://steamcommunity.com/id/johndoe/friends/");

            mockLinkGenerator.Setup(l => l.TransformToUser(It.IsAny<Uri>())).
                Returns("johndoe");

            return new SteamProfileFactory(mockLinkGenerator.Object);
        }

        //[Test]
        //public void Create_With_Name_Correct_Id()
        //{
        //    var sut = Create_SteamProfileFactory();
        //    var profile = sut.Create("johndoe");
        //    var actual = profile.Id;
        //    const string expected = "johndoe";
        //    Assert.AreEqual(expected, actual);
        //}


        //[Test]
        //public void Create_With_Name_Correct_Link()
        //{
        //    var sut = Create_SteamProfileFactory();
        //    var profile = sut.Create("johndoe");
        //    var actual = profile.Link;
        //    const string expected = @"http://steamcommunity.com/id/johndoe/friends/";
        //    Assert.AreEqual(expected, actual);
        //}

        [Test]
        public void Create_With_Link_Correct_Id()
        {
            var sut = Create_SteamProfileFactory();
            var uri = new Uri(@"http://steamcommunity.com/id/johndoe/");
            var profile = sut.Create(uri);
            var actual = profile.Id;
            const string expected = "johndoe";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_With_Link_Correct_Link()
        {
            var sut = Create_SteamProfileFactory();
            var uri = new Uri(@"http://steamcommunity.com/id/johndoe/friends/");
            var profile = sut.Create(uri);
            var actual = profile.Link;
            const string expected = @"http://steamcommunity.com/id/johndoe/friends/";
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Create_With_Name_And_Link_Correct_Id()
        {
            var sut = Create_SteamProfileFactory();
            var uri = new Uri(@"http://steamcommunity.com/id/johndoe/friends/");
            var profile = sut.Create("johndoe", @"http://steamcommunity.com/id/johndoe/friends/");
            var actual = profile.Id;
            const string expected = "johndoe";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_With_Name_And_Link_Correct_Link()
        {
            var sut = Create_SteamProfileFactory();
            var uri = new Uri(@"http://steamcommunity.com/id/johndoe/friends/");
            var profile = sut.Create("johndoe", @"http://steamcommunity.com/id/johndoe/friends/");
            var actual = profile.Link;
            const string expected = @"http://steamcommunity.com/id/johndoe/friends/";
            Assert.AreEqual(expected, actual);
        }

    }
}
