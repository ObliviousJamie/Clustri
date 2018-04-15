using System;
using System.Text.RegularExpressions;
using Castle.Core.Internal;
using Clustri.Crawl.Console.Mfc;
using Clustri.Repository.Core;
using Clustri.Repository.Core.Interfaces;

namespace Clustri.Crawl.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioc = new IocContainer();
            INeo4JUnitOfWork unitOfWork;
            var arguments = ReadInput();
            while (arguments.IsNullOrEmpty())
            {
                arguments = ReadInput();
            }

            if (arguments.Length < 5) unitOfWork = new MemoryUnitOfWorkFactory().Create();
            else unitOfWork = new UnitOfWorkFactory(arguments[2], arguments[3], arguments[4]).Create();

            var userRepo = unitOfWork.Users;


            var mfc = new MfcCrawler(ioc, userRepo, new Printer(5), new CommunityDecider(), 500);

            var link = new Uri(@"http://steamcommunity.com/id/faucomte97");
            mfc.Crawl(int.Parse(arguments[1]), link);

            var output = new Output(userRepo, arguments[0]);
            output.Write();

            System.Console.WriteLine($"Saved output to {arguments[0]} ");
            System.Console.WriteLine("Press Enter to exit");
            System.Console.ReadLine();
        }

        private static string[] ReadInput()
        {

            System.Console.WriteLine("Please enter the following: absolute save directory, number of pages to fully explore");
            var args = System.Console.ReadLine();

            //Accept anything that looks like a link or file but with no space
            var pattern = @"[\w+\\:\/\.]+";
            var matches = Regex.Matches(args, pattern);
            var stringArray = new string[matches.Count];

            if (matches.Count == 0)
                System.Console.WriteLine("No arguments were registered");
            else
            {
                stringArray = new string[matches.Count];
                for (var i = 0; i < matches.Count; i++)
                    stringArray[i] = matches[i].Value;
            }
            return stringArray;
        }
    }
}
