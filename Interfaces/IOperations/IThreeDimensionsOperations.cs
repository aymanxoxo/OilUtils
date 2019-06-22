namespace Interfaces.IOperations
{
    public interface IThreeDimensionsOperations
    {
        double[] Xs { get; set; }

        double[] Ys { get; set; }

        double[] Z1s { get; set; }

        double[] Z2s { get; set; }

        double Volume();
    }
}
