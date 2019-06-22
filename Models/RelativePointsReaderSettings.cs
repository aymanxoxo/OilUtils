using Models.IContracts;

namespace Models
{
    public class RelativePointsReaderSettings : IReaderSettings
    {
        public double[] DependentPoints { get; set; }

        public string DiffValues { get; set; }
    }
}
