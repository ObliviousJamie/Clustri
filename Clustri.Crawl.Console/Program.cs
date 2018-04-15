using System;
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


            if (args.Length < 5)
                unitOfWork = new MemoryUnitOfWorkFactory().Create();
            else
                //unitOfWork = new UnitOfWorkFactory("http://localhost:7474/db/data", args[0], args[1]).Create();
                unitOfWork = new UnitOfWorkFactory(args[2], args[3], args[4]).Create();

            var userRepo = unitOfWork.Users;


            var mfc = new MfcCrawler(ioc, userRepo, new Printer(5), new CommunityDecider(), 500);

            var link = new Uri(@"http://steamcommunity.com/id/faucomte97");
            mfc.Crawl(Int32.Parse(args[1]), link);

            //var output = new Output(userRepo, @"C:\Users\Jamie\Documents\University\Csharp_output");
            var output = new Output(userRepo, args[0]);
            output.Write();

            System.Console.WriteLine($"Saved output to {args[0]} ");

        }
    }
}
