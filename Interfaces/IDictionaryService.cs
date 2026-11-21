namespace exam.Interfaces;
using exam.Models;
using exam.Utils;
public interface IDictionaryService
{
    IReadOnlyList<WordDictionary> Dictionaries { get; }
    bool AddDictionary(WordDictionary dictionary);
    bool RemoveDictionary(string title);
    WordDictionary? FindDictionary(string title);
    List<WordDictionary> FindByLanguage(Language language);
    void ClearDictionaries();
}