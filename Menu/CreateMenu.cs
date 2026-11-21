using exam.Models;

namespace exam.Menu;
using static System.Console;
using exam.Service;
using exam.Utils;
public class CreateMenu
{
    private readonly Manager _manager;
    public CreateMenu(Manager manager)
    {
        _manager = manager;
    }
    public void CreateDictionary()
    {
        WriteLine("Enter a dictionary title: ");
        var title = ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(title))
        {
            WriteLine("Dictionary title cannot be empty.");
            return;
        }
        WriteLine("Available languages: ");
        Language[] languages = Enum.GetValues<Language>();
        for (int i = 0; i < languages.Length; i++)
        {
            WriteLine($"{i + 1}. {languages[i]}");
        }
        WriteLine("Enter a dictionary source language: ");
        var source = ReadLine() ?? "";
        WriteLine("Enter a dictionary target language: ");
        var target = ReadLine() ?? "";
        
        if (!int.TryParse(source, out int sourceChoice) || sourceChoice < 1 || sourceChoice > languages.Length)
        {
            WriteLine("Invalid source language.");
            return;
        }
        Language sourceL = languages[sourceChoice - 1];
        
        if (!int.TryParse(target, out int targetChoice) || targetChoice < 1 || targetChoice > languages.Length)
        {
            WriteLine("Invalid target language.");
            return;
        }
        Language targetL = languages[targetChoice - 1];
        if (sourceL == targetL)
        {
            WriteLine("Source and target languages cannot be the same.");
            return;
        }
        List<NewWord> words = new List<NewWord>();
        WordDictionary dictionary = new WordDictionary(title, sourceL, targetL, words);
        
        string fileName = $"{title}-{sourceL}-{targetL}.json";
        string path = Path.Combine("Data", "Dictionaries", fileName);
        _manager.CreateDictionary(dictionary, path);
    }
}