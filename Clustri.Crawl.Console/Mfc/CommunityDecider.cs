using Clustri.Crawl.Console.Interfaces;

namespace Clustri.Crawl.Console.Mfc
{
    public class CommunityDecider : ICommunityDecider
    {
        private double min = 0.2;
        private double max = 0.4;

        public bool ShouldCreateCommunity(double lastWeight, double currentWeight)
        {
            if (currentWeight < lastWeight)
            {
                var halfDifference = (max - min) / 2.0;
                var nodeDifference = lastWeight - currentWeight;

                if (nodeDifference > halfDifference)
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
