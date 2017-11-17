using System;
using Clustri.Crawl.Console.Interfaces;
using Clustri.Crawl.Crawler.Interfaces;

namespace Clustri.Crawl.Console
{
    public class Printer : IPrinter
    {
        private readonly string whitespace;

        public Printer(int spaces)
        {
            whitespace = new String(' ', spaces);
        }

        public void Print(string text)
        {
            System.Console.WriteLine(text);
        }

        public void Print(IVertex vertex, bool indentation = false)
        {
            if (indentation)
                System.Console.Write(whitespace);

            System.Console.WriteLine($"Name: {vertex.Id}");

            if (indentation)
                System.Console.Write(whitespace);

            System.Console.WriteLine($"Weight: {vertex.Weight} Connections: {vertex.Degree}");
        }

        public void Print(IProfile profile, bool indentation = false)
        {
            if (indentation)
                System.Console.Write(whitespace);

            System.Console.WriteLine($"Name: {profile.Id}");
        }
    }
}
