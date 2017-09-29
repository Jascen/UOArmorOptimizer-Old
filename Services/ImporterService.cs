using System;
using System.Collections.Generic;
using System.IO;

namespace ArmorOptimizer.Services
{
    public class ImporterService
    {
        public string ImportFile(string fileToImport)
        {
            if (string.IsNullOrWhiteSpace(fileToImport)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileToImport));
            if(!File.Exists(fileToImport)) throw new FileNotFoundException($"Failed to locate '{fileToImport}'.");

            var text = File.ReadAllText(fileToImport);
            return text;
        }

        public IEnumerable<string> SplitString(string inputText, char separator)
        {
            return inputText.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}