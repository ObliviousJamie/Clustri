using System;
using System.Text;

namespace Clustri.Crawl.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioc = new IocContainer();
            var crawler = ioc.SteamCrawler(1000, 200);

            var whitespace = new String(' ', 5);
            
            var link = new Uri(@"http://steamcommunity.com/id/faucomte97");

            foreach (var vertex in crawler.Crawl(link))
            {
                System.Console.WriteLine(vertex.Id);
                System.Console.WriteLine("---Links---");
                foreach (var profile in vertex.Degrees)
                {
                    System.Console.WriteLine($"{whitespace}{profile.Id}");
                }

            }
        }
    }
}
