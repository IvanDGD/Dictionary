namespace DictionaryApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFileStorage fileStorage = new FileStorage();
            DictionaryManager manager = new DictionaryManager(fileStorage);
            IMenu menu = new ConsoleMenu(fileStorage);
            DictionaryApp app = new DictionaryApp(menu, fileStorage, manager);
            app.startApp();
        }
    }
}
