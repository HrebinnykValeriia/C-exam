namespace exam.Models;

public class NewWord
{
    public string Word { get; set; }
    public List<string> Translations { get; set; }
    
    public NewWord(string word, List<string> translations)
    {
        Word = word;
        Translations = translations;
    }

    public override string ToString()
    {
        return $"{Word}: {string.Join(", ", Translations)}";
    }
    
}