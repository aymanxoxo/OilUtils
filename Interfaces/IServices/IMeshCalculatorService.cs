namespace Interfaces.IServices
{
    public interface IMeshCalculatorService
    {
        string CalculatePositions();

        int[] CalculateTriangleIndices();

        bool CanDraw();
    }
}
