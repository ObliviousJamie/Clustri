using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using Clustri.Crawl.Crawler;
using Clustri.Crawl.Crawler.Interfaces;
using Moq;
using NUnit.Framework;

namespace Clustri.Crawl.Test.Unit.Crawler
{
    [TestFixture()]
    public class MfcSchedulerTest
    {
        public MfcScheduler Create()
        {
            var mockSet = new Mock<ILogSet<IVertex>>();
            mockSet.Setup(m => m.Exists(It.IsAny<IVertex>())).Returns(false);
            return new MfcScheduler(mockSet.Object, new Dictionary<IVertex, double>());
        }

        private IEnumerable<IVertex> CreateVertices()
        {
            var profilesOne = new List<IProfile> { new Profile("1", "1") };
            var profilesThree = new List<IProfile> { new Profile("1", "1"), new Profile("2", "2"), new Profile("3", "3") };
            var vOne = new Vertex("vOne", profilesOne);
            var vThree = new Vertex("vThree", profilesThree);
            return new List<IVertex> { vOne, vThree };
        }

        [Test]
        public void Return_Highest_Value()
        {
            var sut = Create();
            foreach (var vertex in CreateVertices())
            {
                sut.Add(vertex);
            }
            var expected = CreateVertices().FirstOrDefault();
            sut.Add(expected);
            var actual = sut.FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Return_Highest_Value_Using_Multiple_Add()
        {
            var sut = Create();
            sut.Add(CreateVertices());
            var expected = CreateVertices().FirstOrDefault();
            sut.Add(expected);
            var actual = sut.FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Visited_Updated()
        {
            var mockSet = new Mock<ILogSet<IVertex>>();
            mockSet.Setup(m => m.Exists(It.IsAny<IVertex>())).Returns(false);
            var sut = new MfcScheduler(mockSet.Object, new Dictionary<IVertex, double>());
            foreach (var vertex in CreateVertices())
            {
                sut.Add(vertex);
                sut.Remove(vertex);
            }
            mockSet.Verify(m => m.Add(It.IsAny<IVertex>()), Times.AtLeastOnce());
        }

        [Test]
        public void Reference_Score_Correct()
        {
            var mockSet = new Mock<ILogSet<IVertex>>();
            mockSet.Setup(m => m.Exists(It.IsAny<IVertex>())).Returns(false);
            var dict = new Dictionary<IVertex, double>();
            var sut = new MfcScheduler(mockSet.Object, dict);

            var profilesThree = new List<IProfile> { new Profile("1", "1"), new Profile("2", "2"), new Profile("3", "3") };
            var vThree = new Vertex("vThree", profilesThree);

            double actual;
            double expected = 1.0 / 3.0;

            sut.Add(vThree);
            actual = dict[vThree];
            Assert.AreEqual(expected, actual);

            expected = 2.0 / 3.0;
            sut.Add(vThree);
            actual = dict[vThree];
            Assert.AreEqual(expected, actual);

            expected = 1.0;
            sut.Add(vThree);
            actual = dict[vThree];
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Correct_Order_Lowest_Friend_Number()
        {
            
            var profilesOne = new List<IProfile> { new Profile("1", "1") };
            var profilesTwo = new List<IProfile> { new Profile("1", "1"), new Profile("4", "4") };
            var profilesThree = new List<IProfile> { new Profile("1", "1"), new Profile("2", "2"), new Profile("3", "3") };
            var vOne = new Vertex("vOne", profilesOne);
            var vTwo = new Vertex("vTwo", profilesTwo);
            var vThree = new Vertex("vThree", profilesThree);

            var sut = Create();
            sut.Add(vThree);
            sut.Add(vOne);
            sut.Add(vTwo);
            IVertex expected = sut.FirstOrDefault();
            Assert.AreEqual(expected, vOne);
        }

        [Test]
        public void Correct_Order_Added_Many_Times()
        {
            
            var profilesTwo = new List<IProfile> { new Profile("1", "1"), new Profile("4", "4") };
            var profilesThree = new List<IProfile> { new Profile("1", "1"), new Profile("2", "2"), new Profile("3", "3") };
            var vTwo = new Vertex("vTwo", profilesTwo);
            var vThree = new Vertex("vThree", profilesThree);

            var sut = Create();
            sut.Add(vThree);
            sut.Add(vThree);
            sut.Add(vThree);
            sut.Add(vTwo);
            IVertex expected = sut.FirstOrDefault();
            Assert.AreEqual(expected, vThree);
        }
    }
}
