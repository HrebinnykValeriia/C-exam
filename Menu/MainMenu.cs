namespace exam.Menu;

using static System.Console;
using exam.Service;

public class MainMenu
{
    private readonly UseMenu _useMenu;
    private readonly CreateMenu _createMenu;
    private readonly EditMenu _editMenu;
    private readonly UserMenu _userMenu;
    private readonly Manager _manager;

    public MainMenu(Manager manager)
    {
        _manager = manager;
        _useMenu = new UseMenu(manager);
        _createMenu = new CreateMenu(manager);
        _editMenu = new EditMenu(manager);
        _userMenu = new UserMenu(manager);
    }
    public void Menu()
    {
        string menu = """
                      DICTIONARY MENU:

                      0. Exit
                      1. Use a dictionary
                      2. Create new dictionary
                      3. Edit a dictionary
                      4. See your statistics

                      Your choice:
                      """;

        do
        {
            try
            {
                ForegroundColor = ConsoleColor.Magenta;
                WriteLine(menu);
                ResetColor();

                ForegroundColor = ConsoleColor.Yellow;
                if (!int.TryParse(ReadLine(), out int choice))
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Please enter a number.");
                    ResetColor();
                    continue;
                }
                ResetColor();

                if (choice == 0)
                {
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("Goodbye!");
                    ResetColor();
                    break;
                }

                switch (choice)
                {
                    case 1:
                    {
                        _useMenu.Menu();
                        break;
                    }
                    case 2:
                    {
                        _createMenu.CreateDictionary();
                        break;
                    }
                    case 3:
                    {
                        _editMenu.Menu();
                        break;
                    }
                    case 4:
                    {
                        _userMenu.Statistics();
                        break;
                    }
                    default:
                    {
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine("Invalid choice");
                        ResetColor();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine(e.Message);
                ResetColor();
            }

            ReadKey();
            Clear();

        } while (true);
    }
}