using DictionaryApp;

internal class ConsoleMenu : IMenu
{
    private IFileStorage fileStorage;

    public ConsoleMenu(IFileStorage fileStorage)
    {
        this.fileStorage = fileStorage;
    }

    public void navigateBack()
    {
        Console.WriteLine("Returning to previous menu...");
    }

    public void showDictionaryMenu(IDictionary dictionary)
    {
        Console.WriteLine($"Now managing dictionary: {dictionary.GetType().Name}");
    }

    public void showMainMenu()
    {
        Console.WriteLine("Returning to main menu...");
    }
}
