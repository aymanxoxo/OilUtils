using Models.IContracts;

namespace Interfaces.IServices
{
    public interface ILayerReaderService<T> where T : IReaderSettings
    {
        double[] ReadPoints(T settings);
    }
}
