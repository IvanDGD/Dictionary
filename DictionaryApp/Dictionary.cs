using System;
using System.Collections.Generic;
using System.Linq;

namespace DictionaryApp
{
    internal class Dictionary : IDictionary
    {
        private string languageFrom;
        private string languageTo;
        private Dictionary<string, List<string>> words;

        public Dictionary(string languageFrom, string languageTo)
        {
            this.languageFrom = languageFrom;
            this.languageTo = languageTo;
            this.words = new Dictionary<string, List<string>>();
        }

        public void AddWord(string word, string[] translations)
        {

            if (!words.ContainsKey(word))
            {
                words[word] = new List<string>();
            }

            words[word].AddRange(translations.Where(t => !string.IsNullOrWhiteSpace(t)));
        }

        public void UpdateWord(string oldWord, string newWord)
        {
            if (words.ContainsKey(oldWord))
            {
                var translations = words[oldWord];
                words.Remove(oldWord);
                words[newWord] = translations;
            }
            else
            {
                Console.WriteLine("Word to update not found.");
            }
        }

        public void UpdateTranslation(string word, string oldTranslation, string newTranslation)
        {
            if (words.TryGetValue(word, out var translations))
            {
                int index = translations.IndexOf(oldTranslation);
                if (index >= 0)
                {
                    translations[index] = newTranslation;
                }
                else
                {
                    Console.WriteLine("Old translation not found.");
                }
            }
            else
            {
                Console.WriteLine("Word not found.");
            }
        }


        public void DeleteWord(string word)
        {
            if (words.ContainsKey(word))
            {
                words.Remove(word);
                Console.WriteLine($"Word '{word}' and its translations were removed.");
            }
            else
            {
                Console.WriteLine($"Word '{word}' not found.");
            }
        }

        public void DeleteTranslation(string word, string translation)
        {
            if (words.TryGetValue(word, out var translations))
            {
                if (translations.Count > 1)
                {
                    translations.Remove(translation);
                    Console.WriteLine($"Translation '{translation}' removed from word '{word}'.");
                }
                else
                {
                    Console.WriteLine("Cannot delete the last translation of a word.");
                }
            }
            else
            {
                Console.WriteLine("Word not found.");
            }
        }


        public string[] SearchTranslations(string word)
        {
            if (words.TryGetValue(word, out var translations))
            {
                return translations.ToArray();
            }
            return Array.Empty<string>();
        }

        public Dictionary<string, string[]> GetAllWords()
        {
            return words.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.ToArray()
            );
        }

        public void PrintAllWords()
        {
            if (words.Count == 0)
            {
                Console.WriteLine("The dictionary is empty.");
                return;
            }

            Console.WriteLine($"Words in dictionary ({languageFrom} -> {languageTo}):");
            foreach (var pair in words)
            {
                string translations = string.Join(", ", pair.Value);
                Console.WriteLine($"- {pair.Key}: {translations}");
            }
        }

    }
}