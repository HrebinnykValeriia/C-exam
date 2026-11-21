using exam.Models;
using exam.Interfaces;
using exam.Utils;
namespace exam.Service;

public class DictionaryService : IDictionaryService
{
    private readonly List<WordDictionary> _dictionaries;

    public IReadOnlyList<WordDictionary> Dictionaries => _dictionaries;

    public DictionaryService(List<WordDictionary> dictionaries)
    {
        _dictionaries = dictionaries ?? new List<WordDictionary>();
    }

    
    public bool AddDictionary(WordDictionary dictionary)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        if (_dictionaries.Any(d => d.Title.Equals(dictionary.Title, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }
        _dictionaries.Add(dictionary);
        return true;
    }

    public bool RemoveDictionary(string title)
    {
        WordDictionary? dictionary = FindDictionary(title);
        if (dictionary == null)
        {
            return false;
        }
        return _dictionaries.Remove(dictionary);
    }
    public WordDictionary? FindDictionary(string title)
    {
        return _dictionaries.FirstOrDefault(d => d.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public List<WordDictionary> FindByLanguage(Language language)
    {
        return _dictionaries
            .Where(d => d.SourceLanguage == language || d.TargetLanguage == language)
            .ToList();
    }
    public void ClearDictionaries()
    {
        _dictionaries.Clear();
    }
}
