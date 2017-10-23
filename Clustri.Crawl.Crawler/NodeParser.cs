﻿using System;
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

        public NodeParser(IHyperLinkParser hyperLinkParser, IVertexFactory vertexFactory, IProfileFactory profileFactory)
        {
            _hyperLinkParser = hyperLinkParser;
            _vertexFactory = vertexFactory;
            _profileFactory = profileFactory;
        }

        public IVertex Parse(string userId)
        {
            var profile = _profileFactory.Create(userId);

            //Parse root
            var enumerableLinks = _hyperLinkParser.ParseUser(profile);
            //Parse root children
            var degrees = enumerableLinks.Select(link => _profileFactory.Create(new Uri(link).AbsoluteUri));

            return _vertexFactory.Create(profile, degrees);
        }

        //TODO implement caching of parsed pages
        public IEnumerable<IVertex> ParseFriends(IVertex vertex)
        {
            var list = vertex.Degrees.Select(profile => Parse(profile.Id));
            return list;
        }
    }
}
