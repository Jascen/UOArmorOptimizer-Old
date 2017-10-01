using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace ArmorOptimizer.Services
{
    public class ImporterService
    {
        public T ImportJsonFile<T>(string fileToImport)
        {
            var data = ReadFile(fileToImport);
            if (data.StartsWith("["))
            {
                var jobArray = JArray.Parse(data);
                return jobArray.ToObject<T>();
            }

            var jobObject = JObject.Parse(data);
            return jobObject.ToObject<T>();
        }

        public string ReadFile(string fileToImport)
        {
            if (string.IsNullOrWhiteSpace(fileToImport)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileToImport));
            if (!File.Exists(fileToImport)) throw new FileNotFoundException($"Failed to locate '{fileToImport}'.");

            var text = File.ReadAllText(fileToImport);

            return text;
        }

        public IEnumerable<string> SplitString(string inputText, char separator)
        {
            return inputText.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}