using exam.Service;
using exam.Interfaces;
using exam.Menu;
using exam.Models;

using Serilog;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("../../../Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
try
{
    Log.Information("Program started");
    IFileService fileService = new FileService();
    IDictionaryService dictionaryService = new DictionaryService(new List<WordDictionary>());
    IWordService wordService = new WordService();
    IUserService userService = new UserService(new List<User>());
    IUserFileService userFileService = new UserFileService();
    Manager manager = new Manager(fileService, dictionaryService, wordService, userService, userFileService);
    UserMenu userMenu = new UserMenu(manager);
    manager.LoadUsers();
    manager.LoadDictionaries();
    bool loggedIn = userMenu.Login();
    if (loggedIn)
    {
        MainMenu mainMenu = new MainMenu(manager);
        mainMenu.Menu();
    }
    else
    {
        Console.WriteLine("Goodbye!");
        Log.Warning("User login failed");
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "An error occurred");
}
finally
{
    Log.CloseAndFlush();
}
