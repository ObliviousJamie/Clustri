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
        public int Weight { get; set; } = -1;

        public Vertex(string id, IEnumerable<IProfile> degrees)
        {
            var enumerable = degrees as IProfile[] ?? degrees.ToArray();
            Id = id;
            Degrees = enumerable;
            Degree = enumerable.Length;
        }

        protected bool Equals(Vertex other)
        {
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vertex)obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
