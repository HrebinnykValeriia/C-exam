namespace exam.Menu;
using exam.Service;
using exam.Models;
using static System.Console;
public class EditMenu
{
    private readonly Manager _manager;
    private readonly UseMenu  _useMenu;
    public EditMenu(Manager manager)
    {
        _manager = manager;
        _useMenu = new UseMenu(manager);
    }
    public void AddWord()
    {
        WordDictionary? dictionary = _useMenu.Select();
        if (dictionary == null)
        {
            WriteLine("There are no dictionaries yet.");
            return;
        }
        WriteLine("Adding word...");
        WriteLine("Enter a word: ");
        string word = ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(word))
        {
            WriteLine("Enter a word: ");
            word = ReadLine() ?? string.Empty;
        }

        List<string> trans = new List<string>();
        while (true)
        {
            WriteLine("Enter a translation or nothing when you are done: ");
            string w = ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(w))
            {
                break;
            }
            trans.Add(w);
        }

        if (trans.Count == 0)
        {
            WriteLine("At least one translation is required.");
            return;
        }
        NewWord newWord = new NewWord(word, trans);
        bool added = _manager.AddWord(dictionary, newWord);
        if (added)
        {
            WriteLine("Word added successfully!");
        }
        else
        {
            WriteLine("Word already exists or data is invalid.");
        }
    }

    public void AddTranslation()
    {
        WordDictionary? dictionary = _useMenu.Select();
        if (dictionary == null)
        {
            WriteLine("There are no dictionaries yet.");
            return;
        }
        WriteLine("Enter a word: ");
        string word = ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(word))
        {
            WriteLine("Enter a word: ");
            word = ReadLine() ?? string.Empty;
        }
        WriteLine("Enter a translation: ");
        string translation = ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(translation))
        {
            WriteLine("Enter a translation: ");
            translation = ReadLine() ?? string.Empty;
        }
        bool edit = _manager.AddTranslation(dictionary, word, translation);
        if (edit){
            WriteLine("Translation added successfully!");
            
        }
        else
        {
            WriteLine("Translation already exists or data is invalid.");
        }
    }

    public void RemoveWord()
    {
        WordDictionary? dictionary = _useMenu.Select();
        if (dictionary == null)
        {
            WriteLine("There are no dictionaries yet.");
            return;
        }
        WriteLine("Enter a word: ");
        string word = ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(word))
        {
            WriteLine("Enter a word: ");
            word = ReadLine() ?? string.Empty;
        }
        NewWord? newWord = _manager.FindWord(dictionary, word);
        if (newWord == null)
        {
            WriteLine("Word not found.");
            return;
        }
        WriteLine("Removing word...");
        bool remove = _manager.RemoveWord(dictionary, newWord);
        if (remove)
        {
            WriteLine("Word removed successfully!");
        }
        else
        {
            WriteLine("Word cannot be removed.");
        }
    }
    public void EditWord()
    {
        WordDictionary? dictionary = _useMenu.Select();
        if (dictionary == null)
        {
            WriteLine("There are no dictionaries yet.");
            return;
        }
        WriteLine("Enter an old word: ");
        string oldWord = ReadLine() ?? string.Empty;
        WriteLine("Enter a new word: ");
        string newWord = ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(oldWord))
        {
            WriteLine("Enter an old word again: ");
            oldWord = ReadLine() ?? string.Empty;
        }
        while (string.IsNullOrWhiteSpace(newWord))
        {
            WriteLine("Enter a new word again: ");
            newWord = ReadLine() ?? string.Empty;
        }

        bool edit = _manager.EditWord(dictionary, oldWord, newWord);
        if (edit)
        {
            WriteLine("Word edited successfully!");
        }
        else
        {
            WriteLine("Word cannot be edited.");
        }
    }

    public void RemoveTranslation()
    {
        WordDictionary? dictionary = _useMenu.Select();
        if (dictionary == null)
        {
            WriteLine("There are no dictionaries yet.");
            return;
        }
        WriteLine("Enter a word: ");
        string word = ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(word))
        {
            WriteLine("Enter a word: ");
            word = ReadLine() ?? string.Empty;
        }
        WriteLine("Enter a translation: ");
        string translation = ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(translation))
        {
            WriteLine("Enter a translation: ");
            translation = ReadLine() ?? string.Empty;
        }
        bool remove = _manager.RemoveTranslation(dictionary, word, translation);
        if (remove)
        {
            WriteLine("Translation removed successfully!");
        }
        else
        {
            WriteLine("Translation not found or it is the last translation.");
        }
    }

    public void EditTranslation()
    {
        WordDictionary? dictionary = _useMenu.Select();
        if (dictionary == null)
        {
            WriteLine("There are no dictionaries yet.");
            return;
        }
        WriteLine("Enter a word: ");
        string word = ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(word))
        {
            WriteLine("Enter a word again: ");
            word = ReadLine() ?? string.Empty;
        }
        WriteLine("Enter a translation you want to edit: ");
        string translation = ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(translation))
        {
            WriteLine("Enter a translation you want to edit: ");
            translation = ReadLine() ?? string.Empty;
        }
        WriteLine("Enter a new translation: ");
        string translation2 = ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(translation2))
        {
            WriteLine("Enter a new translation again: ");
            translation2 = ReadLine() ?? string.Empty;
        }
       bool edit = _manager.EditTranslation(dictionary, word, translation, translation2);
       if (edit)
       {
           WriteLine("Translation edited successfully!");
       }
       else
       {
           WriteLine("Translation cannot be edited.");
       }
    }

    public void RemoveDictionary()
    {
        WordDictionary? dictionary = _useMenu.Select();
        if (dictionary == null)
        {
            WriteLine("There are no dictionaries yet.");
            return;
        }
        bool delete = _manager.DeleteDictionary(dictionary.Title);
        if (delete)
        {
            WriteLine("Dictionary deleted successfully!");
        }
        else
        {
            WriteLine("Dictionary cannot be deleted.");
        }
    }
    
    public void Menu()
    {
        string menu = """
                      EDIT MENU:

                      0. Come back to main menu
                      1. Add a word
                      2. Remove a word
                      3. Edit a word
                      4. Add a translation
                      5. Remove a translation
                      6. Edit a translation
                      7. Delete dictionary
                      
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
                        AddWord();
                        break;
                    case 2:
                    {
                       RemoveWord();
                        break;
                    }
                    case 3:
                    {
                        EditWord();
                        break;
                    }
                    case 4:
                        AddTranslation();
                        break;
                    case 5:
                        RemoveTranslation();
                        break;
                    case 6:
                        EditTranslation();
                        break;
                    case 7:
                        RemoveDictionary();
                        break;
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