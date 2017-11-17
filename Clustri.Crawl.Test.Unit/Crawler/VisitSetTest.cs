using System.Collections.Generic;
using Clustri.Crawl.Crawler;
using Clustri.Crawl.Crawler.Interfaces;
using NUnit.Framework;

namespace Clustri.Crawl.Test.Unit.Crawler
{
    [TestFixture]
    public class VisitSetTest
    {

        private VisitSet<IVertex> Create()
        {
            return new VisitSet<IVertex>();
        }


        [Test]
        public void Returns_True_On_Exists()
        {
            var vertex = new Vertex("johndoe", new List<IProfile>{new Profile("one", "two")});
            var sut = Create();
            sut.Add(vertex);
            var exists = sut.Exists(vertex);
            Assert.IsTrue(exists);
        }

        [Test]
        public void Returns_False_On_Not_Exists()
        {
            var vertex = new Vertex("johndoe", new List<IProfile>{new Profile("one", "two")});
            var sut = Create();
            var exists = sut.Exists(vertex);
            Assert.IsFalse(exists);
        }
    }
}
