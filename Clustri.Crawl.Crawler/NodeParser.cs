using System;
using System.Collections.Generic;
using System.Linq;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Crawler
{
    public class NodeParser : INodeParser
    {
        private readonly IHyperLinkParser _hyperLinkParser;
        private readonly IVertexFactory _vertexFactory;
        private readonly IProfileFactory _profileFactory;
        private readonly IVertexCache _cache;
        private readonly IPause _pause;

        public NodeParser(IHyperLinkParser hyperLinkParser, IVertexFactory vertexFactory,
            IProfileFactory profileFactory, IVertexCache cache, IPause pause)
        {
            _hyperLinkParser = hyperLinkParser;
            _vertexFactory = vertexFactory;
            _profileFactory = profileFactory;
            _cache = cache;
            _pause = pause;
        }

        public IVertex Parse(string userId)
        {
            if (InCache(userId, out IVertex vertex))
                return vertex;

            var profile = _profileFactory.Create(userId);

            //Wait before processing
            _pause.Pause();
            //Parse root
            var enumerableLinks = _hyperLinkParser.ParseUser(profile);
            //Parse root children
            var degrees = enumerableLinks.Select(link => _profileFactory.Create(new Uri(link).AbsoluteUri));

            vertex = _vertexFactory.Create(profile, degrees);
            _cache.Save(vertex);

            return vertex;
        }

        public IEnumerable<IVertex> ParseFriends(IVertex vertex)
        {
            return vertex.Degrees.Select(profile => Parse(profile.Id));
        }

        private bool InCache(string id, out IVertex vertex)
        {
            vertex = _cache.Retrieve(id);
            var exists = vertex != null;
            return exists;
        }
    }
}
