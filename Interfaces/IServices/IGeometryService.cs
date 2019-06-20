namespace Interfaces.IServices
{
    public interface IGeometryService
    {
        string Positions();

        int[] TriangleIndices();

        bool CanDraw();
    }
}
