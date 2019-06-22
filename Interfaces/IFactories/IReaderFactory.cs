
using Interfaces.IServices;
using Models.IContracts;

namespace Interfaces.IFactories
{
    public interface IReaderFactory<T> where T: IReaderSettings
    {
        ILayerReaderService GetReaderService(T settings);
    }
}
