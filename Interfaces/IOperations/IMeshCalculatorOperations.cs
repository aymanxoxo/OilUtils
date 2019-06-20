namespace Interfaces.IOperations
{
    public interface IMeshCalculatorOperations
    {
        string CalculatePositions();

        int[] CalculateTriangleIndices();

        bool CanDraw();
    }
}
