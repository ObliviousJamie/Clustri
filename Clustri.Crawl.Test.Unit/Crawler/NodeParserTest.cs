using System;
using System.Collections.Generic;
using System.Linq;
using Clustri.Crawl.Crawler;
using Clustri.Crawl.Crawler.Interfaces;
using Moq;
using NUnit.Framework;

namespace Clustri.Crawl.Test.Unit.Crawler
{
    [TestFixture()]
    public class NodeParserTest
    {
        private NodeParser Create_Node_Parser()
        {
            var mockVertexFactory = new Mock<IVertexFactory>();
            var mockProfileFactory = new Mock<IProfileFactory>();
            var mockHyperlinkParser = new Mock<IHyperLinkParser>();
            var mockVertexCache = new Mock<IVertexCache>();
            var mockPause = new Mock<IPause>();

            mockVertexFactory.Setup(v => v.Create(It.IsAny<IProfile>(), It.IsAny<IEnumerable<IProfile>>()))
                .Returns(new Vertex("johndoe", Create_Profiles()));

            mockProfileFactory.Setup(p => p.Create(It.IsAny<Uri>()))
                .Returns(new Profile("johndoe", @"http://steamcommunity.com/id/johndoe/friends/"));

            mockHyperlinkParser.Setup(h => h.ParseUser(It.IsAny<IProfile>()))
                .Returns(new List<string> { @"http://steamcommunity.com/id/one/friends/", "http://steamcommunity.com/id/two/friends/" });

            mockVertexCache.Setup(v => v.Retrieve(It.IsAny<string>()))
                .Returns((IVertex)null);


            return new NodeParser(mockHyperlinkParser.Object, mockVertexFactory.Object, mockProfileFactory.Object,
                mockVertexCache.Object, mockPause.Object);
        }

        private IEnumerable<IProfile> Create_Profiles()
        {
            return new List<IProfile>
            {
                new Profile("one", @"http://steamcommunity.com/id/one/friends/"),
                new Profile("two", @"http://steamcommunity.com/id/two/friends/")
            };
        }

        private IVertex Create_Vertex()
        {
            return new Vertex("johndoe", Create_Profiles());
        }


        [Test]
        public void Creates_With_Correct_Profile()
        {
            var sut = Create_Node_Parser();
            var profile = sut.Parse(new Uri(@"http://steamcommunity.com/id/johndoe"));
            var actual = profile.Id;
            const string expected = "johndoe";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Creates_With_Correct_Degree()
        {
            var sut = Create_Node_Parser();
            var profile = sut.Parse(new Uri(@"http://steamcommunity.com/id/johndoe"));
            var actual = profile.Degree;
            const int expected = 2;
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Creates_With_Correct_DegreeItem()
        {
            var sut = Create_Node_Parser();
            var profile = sut.Parse(new Uri(@"http://steamcommunity.com/id/johndoe"));
            var actual = profile.Degrees.ToArray()[0].Link;
            const string expected = @"http://steamcommunity.com/id/one/friends/";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Creates_With_Enumerable()
        {
            var sut = Create_Node_Parser();
            var enumerable = sut.ParseFriends(Create_Vertex());
            var actual = enumerable.FirstOrDefault().Id;
            const string expected = @"johndoe";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Saves_To_Cache()
        {
            var mockVertexFactory = new Mock<IVertexFactory>();
            var mockProfileFactory = new Mock<IProfileFactory>();
            var mockHyperlinkParser = new Mock<IHyperLinkParser>();
            var mockVertexCache = new Mock<IVertexCache>();
            var mockPause = new Mock<IPause>();

            mockVertexFactory.Setup(v => v.Create(It.IsAny<IProfile>(), It.IsAny<IEnumerable<IProfile>>()))
                .Returns(new Vertex("johndoe", Create_Profiles()));

            mockProfileFactory.Setup(p => p.Create(It.IsAny<Uri>()))
                .Returns(new Profile("johndoe", @"http://steamcommunity.com/id/johndoe/friends/"));

            mockHyperlinkParser.Setup(h => h.ParseUser(It.IsAny<IProfile>()))
                .Returns(new List<string> { @"http://steamcommunity.com/id/one/friends/", "http://steamcommunity.com/id/two/friends/" });

            mockVertexCache.Setup(v => v.Retrieve(It.IsAny<string>()))
                .Returns((IVertex)null);

            var nodeParser =  new NodeParser(mockHyperlinkParser.Object, mockVertexFactory.Object, mockProfileFactory.Object,
                mockVertexCache.Object, mockPause.Object);

            var vertex = Create_Vertex();
            var friends = nodeParser.Parse(new Uri($"http://steamcommunity.com/id/{vertex.Id}"));
            mockVertexCache.Verify(v => v.Save(vertex));
        }


        [Test]
        public void Gets_From_Cache()
        {
            var mockVertexFactory = new Mock<IVertexFactory>();
            var mockProfileFactory = new Mock<IProfileFactory>();
            var mockHyperlinkParser = new Mock<IHyperLinkParser>();
            var mockVertexCache = new Mock<IVertexCache>();
            var mockPause = new Mock<IPause>();

            mockVertexFactory.Setup(v => v.Create(It.IsAny<IProfile>(), It.IsAny<IEnumerable<IProfile>>()))
                .Returns(new Vertex("johndoe", Create_Profiles()));

            mockProfileFactory.Setup(p => p.Create(It.IsAny<Uri>()))
                .Returns(new Profile("johndoe", @"http://steamcommunity.com/id/johndoe/friends/"));

            mockHyperlinkParser.Setup(h => h.ParseUser(It.IsAny<IProfile>()))
                .Returns(new List<string> { @"http://steamcommunity.com/id/one/friends/", "http://steamcommunity.com/id/two/friends/" });

            mockVertexCache.Setup(v => v.Retrieve(It.IsAny<string>()))
                .Returns((IVertex)null);

            var nodeParser =  new NodeParser(mockHyperlinkParser.Object, mockVertexFactory.Object, mockProfileFactory.Object,
                mockVertexCache.Object, mockPause.Object);

            var vertex = Create_Vertex();
            nodeParser.Parse(new Uri($"http://steamcommunity.com/id/{vertex.Id}"));
            mockVertexCache.Verify(v => v.Retrieve(vertex.Id));
        }

    }
}
