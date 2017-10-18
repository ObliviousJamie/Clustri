using System;
using System.Collections.Generic;
using System.Text;

namespace Clustri.Crawl.Crawler.Interfaces
{
    public interface IUnexploredVertex : IVertex
    {
        IEnumerable<string> ParsedRelationships { get; }
    }
}
