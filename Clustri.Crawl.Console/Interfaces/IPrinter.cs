using System;
using System.Collections.Generic;
using System.Text;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Console.Interfaces
{
    public interface IPrinter
    {
        void Print(string text);
        void Print(IVertex vertex, bool whitespace = false);
        void Print(IProfile profile, bool whitespace = false);
    }
}
