using Clustri.Crawl.Console.Interfaces;
using Clustri.Repository.Implementation.Repository;

namespace Clustri.Crawl.Console.Mfc
{
    public class CommunityDecider : ICommunityDecider
    {
        private double min = 0.2;
        private double max = 0.4;

        public bool ShouldCreateCommunity(double lastWeight, double currentWeight, double peakThreshold = 0.1)
        {
            if (currentWeight < lastWeight)
            {
                var peakAddition = currentWeight + (currentWeight * peakThreshold) ;

                //Check current weight is X percent less than last value
                if (peakAddition < lastWeight)
                {
                    SetWeight(currentWeight);
                    return true;
                }
            }
            SetWeight(currentWeight);

            return false;
        }

        private void SetWeight(double weight)
        {
            min = (weight < min) ? weight : min;
            max = (weight > max) ? weight : max;
        }
    }


}
