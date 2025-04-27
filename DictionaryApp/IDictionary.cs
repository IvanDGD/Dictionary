using System;

namespace DictionaryApp
{
    internal interface IDictionary
    {
        void AddWord(string word, string[] translations);
        void UpdateWord(string oldWord, string newWord);
        void UpdateTranslation(string word, string oldTranslation, string newTranslation);
        void DeleteWord(string word);
        void DeleteTranslation(string word, string translation);
        string[] SearchTranslations(string word);
        Dictionary<string, string[]> GetAllWords();
        void PrintAllWords();
    }
}
