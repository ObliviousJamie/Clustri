using System.Collections.Generic;
using System.Linq;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class VertexCache : IVertexCache
    {
        private readonly int _sizeLimit;

        private IDictionary<string,IVertex> vertices;

        public VertexCache(int sizeLimit)
        {
            vertices = new Dictionary<string, IVertex>(sizeLimit);
            _sizeLimit = sizeLimit;
        }

        public void Save(IVertex vertex)
        {
            var size = vertices.Count;
            if (size > _sizeLimit)
                vertices.Remove(vertices.ElementAt(size - 1));
            vertices[vertex.Id] = vertex;
        }

        public IVertex Retrieve(string id)
        {
            IVertex vertex = null;
            vertices.TryGetValue(id, out vertex);
            return vertex;
        }

    }
}
