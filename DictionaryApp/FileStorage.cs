
using System.IO;
namespace DictionaryApp
{
    internal class FileStorage : IFileStorage
    {
        public void exportTranslations(string word, string[] translations, string exportPath)
        {
            File.WriteAllText(exportPath, $"{word}: {string.Join(", ", translations)}");
        }

        public Dictionary loadDictionary(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            Dictionary dictionary = new Dictionary("Base", "Base");

            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    string word = parts[0].Trim();
                    string[] translations = parts[1].Split(',').Select(t => t.Trim()).ToArray();
                    dictionary.AddWord(word, translations);
                }
            }

            return dictionary;
        }

        public void saveDictionary(IDictionary dictionary, string filePath)
        {
            var data = dictionary.GetAllWords();
            List<string> lines = new List<string>();

            foreach (var pair in data)
            {
                lines.Add($"{pair.Key}: {string.Join(", ", pair.Value)}");
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}