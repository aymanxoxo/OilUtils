using Interfaces.IContracts;
using Interfaces.IServices;

namespace Interfaces.IFactories
{
    public interface IReaderFactory<T> where T: IReaderSettings
    {
        ILayerReaderService GetReaderService(T settings);
    }
}
