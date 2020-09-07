using System;

namespace Algorithm.Composition
{
    public class HighPassSummingAggregator
    {
        PointsAggregator highPassSumming;
        public HighPassSummingAggregator(Measurement[] measurements)
        {
            highPassSumming = new PointsAggregator(measurements, new HighPassFilter(), new SummingStrategy());
        }

        public virtual Measurement Aggregate()
        {
            return highPassSumming.Aggregate();
        }
    }
}
