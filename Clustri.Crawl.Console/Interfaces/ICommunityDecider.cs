namespace Clustri.Crawl.Console.Interfaces
{
    public interface ICommunityDecider
    {
        bool ShouldCreateCommunity(double lastWeight, double currentWeight);
    }
}
