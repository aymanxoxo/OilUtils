using Models.IContracts;

namespace Models
{
    public class IntervalReaderSettings : IReaderSettings
    {
        public double Interval { get; set; }

        public int PointsCount { get; set; }

        public double StartPoint { get; set; }
    }
}
