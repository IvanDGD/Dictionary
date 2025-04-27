using System;
using System.Collections.Generic;
using System.Linq;

namespace DictionaryApp
{
    internal class DictionaryApp
    {
        private IMenu menu;
        private IFileStorage fileStorage;
        private DictionaryManager dictionaryManager;

        public DictionaryApp(IMenu menu, IFileStorage file, DictionaryManager dictionaryManager)
        {
            this.menu = menu;
            fileStorage = file;
            this.dictionaryManager = dictionaryManager;
        }

        public void startApp()
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add dictionary");
                Console.WriteLine("2. Go to existing dictionary");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                string input = Console.ReadLine();
                int choice = 0;
                int.TryParse(input, out choice);

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter language from: ");
                        string languageFrom = Console.ReadLine();
                        Console.Write("Enter language to: ");
                        string languageTo = Console.ReadLine();

                        IDictionary newDict = dictionaryManager.CreateDictionary(languageFrom, languageTo);
                        Console.WriteLine($"Dictionary {languageFrom}-{languageTo} created.");

                        Console.Write("Enter file path to save new dictionary: ");
                        string savePath = Console.ReadLine();
                        fileStorage.saveDictionary(newDict, savePath);
                        Console.WriteLine("Dictionary saved.");

                        InDictionary();
                        break;

                    case 2:
                        Console.Write("Enter path to file: ");
                        string filePath = Console.ReadLine();
                        IDictionary loadedDict = fileStorage.loadDictionary(filePath);

                        if (loadedDict != null)
                        {
                            dictionaryManager.SetCurrentDictionary(loadedDict);
                            Console.WriteLine("Dictionary loaded successfully.");
                            InDictionary();
                        }
                        else
                        {
                            Console.WriteLine("Failed to load dictionary.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Exiting application.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void InDictionary()
        {
            Console.WriteLine("");
            while (true)
            {
                Console.WriteLine("\nDictionary Menu:");
                Console.WriteLine("1. Add word");
                Console.WriteLine("2. Replace a word or its translation");
                Console.WriteLine("3. Remove a word or its translation");
                Console.WriteLine("4. Search a word translation");
                Console.WriteLine("5. Save dictionary to file");
                Console.WriteLine("6. Export a word and its translations to a separate file");
                Console.WriteLine("7. Show all words and translations");
                Console.WriteLine("8. Exit to main menu");

                Console.Write("Choose an option: ");
                string input = Console.ReadLine();
                int choice = 0;
                int.TryParse(input, out choice);

                IDictionary currentDict = dictionaryManager.GetCurrentDictionary();

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter word: ");
                        string newWord = Console.ReadLine();
                        Console.Write("Enter translations: ");
                        string[] translations = Console.ReadLine().Split(", ").Select(t => t.Trim()).ToArray();
                        currentDict.AddWord(newWord, translations);
                        Console.WriteLine("Word added.");
                        break;

                    case 2:
                        Console.Write("Replace (1) Word or (2) Translation? ");
                        string subChoice = Console.ReadLine();
                        if (subChoice == "1")
                        {
                            Console.Write("Enter old word: ");
                            string oldWord = Console.ReadLine();
                            Console.Write("Enter new word: ");
                            string newWordName = Console.ReadLine();
                            currentDict.UpdateWord(oldWord, newWordName);
                            Console.WriteLine("Word updated.");
                        }
                        else if (subChoice == "2")
                        {
                            Console.Write("Enter word: ");
                            string word = Console.ReadLine();
                            Console.Write("Enter old translation: ");
                            string oldTranslation = Console.ReadLine();
                            Console.Write("Enter new translation: ");
                            string newTranslation = Console.ReadLine();
                            currentDict.UpdateTranslation(word, oldTranslation, newTranslation);
                            Console.WriteLine("Translation updated.");
                        }
                        break;

                    case 3:
                        Console.Write("Delete (1) Word or (2) Translation? ");
                        string delChoice = Console.ReadLine();
                        if (delChoice == "1")
                        {
                            Console.Write("Enter word: ");
                            string word = Console.ReadLine();
                            currentDict.DeleteWord(word);
                        }
                        else if (delChoice == "2")
                        {
                            Console.Write("Enter word: ");
                            string word = Console.ReadLine();
                            Console.Write("Enter translation to delete: ");
                            string translation = Console.ReadLine();

                            string[] currentTranslations = currentDict.SearchTranslations(word);
                            TranslationValidator validator = new TranslationValidator();
                            if (validator.canDeleteTranslation(currentTranslations))
                            {
                                currentDict.DeleteTranslation(word, translation);
                                Console.WriteLine("Translation deleted.");
                            }
                            else
                            {
                                Console.WriteLine("Cannot delete the only translation.");
                            }
                        }
                        break;

                    case 4:
                        Console.Write("Enter word to search: ");
                        string searchWord = Console.ReadLine();
                        string[] result = currentDict.SearchTranslations(searchWord);
                        if (result.Length == 0)
                        {
                            Console.WriteLine("No translations found.");
                        }
                        else
                        {
                            Console.WriteLine($"Translations: {string.Join(", ", result)}");
                        }
                        break;

                    case 5:
                        Console.Write("Enter file path to save: ");
                        string savePath = Console.ReadLine();
                        dictionaryManager.SaveCurrentDictionary(savePath);
                        Console.WriteLine("Dictionary saved.");
                        break;

                    case 6:
                        Console.Write("Enter word to export: ");
                        string exportWord = Console.ReadLine();
                        string[] exportTranslations = currentDict.SearchTranslations(exportWord);
                        Console.Write("Enter export file path: ");
                        string exportPath = Console.ReadLine();
                        fileStorage.exportTranslations(exportWord, exportTranslations, exportPath);
                        Console.WriteLine("Exported successfully.");
                        break;

                    case 7:
                        currentDict.PrintAllWords();
                        break;

                    case 8:
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
