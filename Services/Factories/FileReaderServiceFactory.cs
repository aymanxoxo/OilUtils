using Interfaces.IFactories;
using Interfaces.IServices;
using Models;

namespace Services.Factories
{
    public class FileReaderServiceFactory : IReaderFactory<FileReaderSettings>
    {
        public ILayerReaderService GetReaderService(FileReaderSettings settings)
        {
            return new FileReaderService(settings.FilePath);
        }
    }
}
