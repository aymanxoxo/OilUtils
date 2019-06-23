using Infrastructure.Extensions;
using Interfaces.IServices;
using Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Services
{
    public class FileReaderService : ILayerReaderService<FileReaderSettings>
    {
        public double[] ReadPoints(FileReaderSettings settings)
        {
            if (string.IsNullOrEmpty(settings.FilePath) || !File.Exists(settings.FilePath))
            {
                throw new ArgumentException();
            }

            var depthsStr = File.ReadAllText(settings.FilePath);

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
