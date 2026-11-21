namespace exam.Models;
using exam.Utils;
public class WordDictionary
{
    public string Title { get; set; }
    public Language SourceLanguage { get; set; }
    public Language TargetLanguage { get; set; }
    public List<NewWord> Words { get; set; }
    
    public WordDictionary(string title, Language sourceLanguage, Language targetLanguage, List<NewWord> words)
    {
        Title = title;
        SourceLanguage = sourceLanguage;
        TargetLanguage = targetLanguage;
        Words = words;
    }

    public override string ToString()
    {
        return $"\n{Title} ({SourceLanguage} - {TargetLanguage}) contains {Words.Count} words";
    }

    
}