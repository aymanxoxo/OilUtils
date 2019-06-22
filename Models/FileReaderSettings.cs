using Models.IContracts;

namespace Models
{
    public class FileReaderSettings : IReaderSettings
    {
        public string FilePath { get; set; }
    }
}
