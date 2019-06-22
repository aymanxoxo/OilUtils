using Infrastructure.Extensions;
using Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.IO;

namespace Services
{
    public class FileReaderService : ILayerReaderService
    {
        private readonly string _filePath;

        public FileReaderService(string filePath)
        {
            _filePath = filePath;
        }

        public double[] ReadPoints()
        {
            if (!File.Exists(_filePath))
            {
                throw new ArgumentException();
            }

            var depthsStr = File.ReadAllText(_filePath);

            var charsToReplace = new Dictionary<string, string>();
            charsToReplace.Add("\r\n", " ");
            try
            {
                return depthsStr.ExtractDoubles(replaceChars: charsToReplace, separator: ' ');
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
