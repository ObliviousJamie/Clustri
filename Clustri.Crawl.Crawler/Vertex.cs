using System.Collections.Generic;
using System.Linq;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class Vertex : IVertex
    {
        public int Degree { get; }
        public string Id { get; }
        public IEnumerable<IProfile> Degrees { get; }

        public Vertex(string id, IEnumerable<IProfile> degrees)
        {
            var enumerable = degrees as IProfile[] ?? degrees.ToArray();
            Id = id;
            Degrees = enumerable;
            Degree = enumerable.Length;
        }
    }
}
