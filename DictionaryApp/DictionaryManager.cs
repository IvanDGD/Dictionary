namespace DictionaryApp
{
    internal class DictionaryManager
    {
        private IDictionary currentDictionary;
        private IFileStorage fileStorage;

        public DictionaryManager(IFileStorage fileStorage)
        {
            this.fileStorage = fileStorage;
        }

        public IDictionary CreateDictionary(string languageFrom, string languageTo)
        {
            currentDictionary = new Dictionary(languageFrom, languageTo);
            return currentDictionary;
        }

        public void SetCurrentDictionary(IDictionary dictionary)
        {
            currentDictionary = dictionary;
        }

        public IDictionary GetCurrentDictionary()
        {
            return currentDictionary;
        }

        public void SaveCurrentDictionary(string filePath)
        {
            if (currentDictionary != null)
            {
                fileStorage.saveDictionary(currentDictionary, filePath);
            }
        }
    }
}
