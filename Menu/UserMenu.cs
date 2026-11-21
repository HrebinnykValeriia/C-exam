
namespace exam.Menu;

using exam.Models;
using static System.Console;
using exam.Service;
using exam.Utils;

public class UserMenu
{
    private readonly Manager _manager;
    public UserMenu(Manager manager)
    {
        _manager = manager;
    }

    public bool Login()
    {
        while (true)
        {
            ForegroundColor = ConsoleColor.Magenta;
            WriteLine("Login...");
            ResetColor();

            ForegroundColor = ConsoleColor.Yellow;
            Write("Enter your username: ");
            ResetColor();
            string username = ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(username))
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Username cannot be empty!");
                ResetColor();
                return false;
            }

            User? user = _manager.FindUser(username);
            if (user == null)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("User not found!");
                ResetColor();
                ForegroundColor = ConsoleColor.Yellow;
                Write("Would you like to create a new account? (yes/no): ");
                ResetColor();
                string answer = ReadLine() ?? "";

                if (answer.Equals("yes", StringComparison.OrdinalIgnoreCase))
                {
                    ForegroundColor = ConsoleColor.Yellow;
                    Write("Enter your password: ");
                    ResetColor();
                    string password = ReadLine() ?? "";

                    try
                    {
                        _manager.CreateUser(username, password);

                        ForegroundColor = ConsoleColor.Green;
                        WriteLine("User created! Please log in.");
                        ResetColor();

                        continue;
                    }
                    catch (ArgumentException ex)
                    {
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine(ex.Message);
                        ResetColor();
                        return false;
                    }
                }

                ForegroundColor = ConsoleColor.Red;
                WriteLine("Invalid username!");
                ResetColor();
                return false;
            }

            ForegroundColor = ConsoleColor.Yellow;
            Write("Enter your password: ");
            ResetColor();
            string enteredPassword = ReadLine() ?? "";
            if (!_manager.CheckPassword(username, enteredPassword))
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Incorrect password!");
                ResetColor();
                return false;
            }

            _manager.RememberUser(username);
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Welcome back " + username + "!");
            ResetColor();
            return true;
        }
    }

    public void Statistics()
    {
        Dictionary<string, int> statistics = _manager.GetCounter();
        if (statistics.Count == 0)
        {
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("No statistics yet.");
            ResetColor();
            return;
        }

        ForegroundColor = ConsoleColor.Magenta;
        WriteLine("Your statistics:");
        ResetColor();
        foreach (var item in statistics)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine($"{item.Key}: {item.Value}");
            ResetColor();
        }
    }
}
