using exam.Models;
using exam.Service;
using static System.Console;
namespace exam.Menu;
public class UseMenu
{
    private readonly Manager _manager;
    public UseMenu(Manager manager)
    {
        _manager = manager;
    }

    public void ShowDictionaries()
    {
        var dictionaries = _manager.GetDictionaries();
        if (dictionaries.Count == 0)
        {
            WriteLine("No dictionaries found.");
            return;
        }
        int index = 1;
        foreach (var dictionary in dictionaries)
        {
            WriteLine($"{index}: {dictionary}");
            index++;
        }
    }

    public WordDictionary? Select()
    {
        if (_manager.GetDictionaries().Count == 0)
        {
            WriteLine("No dictionaries found.");
            return null;
        }
        WriteLine("Available dictionaries: ");
        ShowDictionaries();
        WriteLine("Enter your choice:");
        if (!int.TryParse(ReadLine(), out int choice))
        {
            WriteLine("Please enter a number.");
            return null;
        }
        if (choice <= 0 || choice > _manager.GetDictionaries().Count)
        {
            WriteLine("Invalid choice.");
            return null;
        }
        return _manager.GetDictionaries()[choice -1];
    }
    public void Find(WordDictionary dictionary)
    {
        while (true)
        {
            Clear();
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("You are using the following dictionary:");
            WriteLine($"\t{dictionary.Title}");
            WriteLine($"{dictionary.SourceLanguage} - {dictionary.TargetLanguage}");
            WriteLine("-----------------------------------------------------------");
            WriteLine("Enter the word you want to find (or 0 to go back):");
            string word = ReadLine() ?? string.Empty;
            if (word == "0")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(word))
            {
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("Please enter a word.");
                continue;
            }
            _manager.CounterPlus(dictionary.Title);
            NewWord? searchWord = _manager.FindWord(dictionary, word);
            if (searchWord == null)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("No word found.");
            }
            else
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine(searchWord);
            }
            WriteLine();
            WriteLine("Press Enter to search for another word...");
            ReadLine();
        }
    }

    public void Export()
    {
        WordDictionary? dictionary = Select();
        if (dictionary == null)
        {
            WriteLine("No dictionary found.");
            return;
        }
        WriteLine("Which word would you like to export: ");
        string word = ReadLine() ?? string.Empty;
       NewWord? newWord = _manager.FindWord(dictionary, word);
       if (newWord == null)
       {
           WriteLine("No word found.");
           return;
       }

       string fileName = $"{newWord.Word}.txt";
       string path = Path.Combine("../../../Data", "Exports", fileName);
       _manager.ExportWord(newWord, path);
       WriteLine($"Exported {fileName} to {path}");
    }

    public void Menu()
    {
        string menu = """
                      MENU:

                      0.Come back to main menu
                      1.Show all dictionaries
                      2.Search for a translation
                      3.Save the word to the file

                      Your choice: 
                      """;
        do
        {
            try
            {
                WriteLine(menu);
                if (!int.TryParse(ReadLine(), out int choice))
                {
                    WriteLine("Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 0:
                    { 
                        return;
                    }
                    case 1:
                        ShowDictionaries();
                        break;
                    case 2:
                    {
                        WordDictionary? dictionary = Select();
                        if (dictionary == null)
                        {
                            break;
                        }
                        Find(dictionary);
                        break;
                    }
                    case 3:
                    {
                        Export();
                        break;
                    }
                    default:
                    {
                        WriteLine("Invalid choice");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
            ReadKey();
            Clear();
        } while (true);
    }
}