namespace exam.Service;
using exam.Models;
using exam.Interfaces;
public class WordService : IWordService
{
    public bool AddWord(WordDictionary dictionary, NewWord word)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        ArgumentNullException.ThrowIfNull(word);
        if (string.IsNullOrWhiteSpace(word.Word) || word.Translations.Count == 0 || word.Translations
                .Any(string.IsNullOrWhiteSpace))
        {
            return false;
        }

        if (FindWord(dictionary, word.Word) != null)
        {
            return false;
        }
        dictionary.Words.Add(word);
        return true;
    }

    public bool RemoveWord(WordDictionary dictionary, NewWord word)
    {
        NewWord? foundWord = FindWord(dictionary, word.Word);
        if (foundWord == null)
        {
            return false;
        }
        dictionary.Words.Remove(foundWord);
        return true;
    }

    public NewWord? FindWord(WordDictionary dictionary, string word)
    {
        return dictionary.Words.FirstOrDefault(w => w.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
    }
    
    public bool AddTranslation(WordDictionary dictionary, string word, string translation)
    {
        NewWord? foundWord = FindWord(dictionary, word);
        if (foundWord == null || string.IsNullOrWhiteSpace(translation))
        {
            return false;
        }
        bool exists = foundWord.Translations.Any(t => t.Equals(translation, StringComparison.OrdinalIgnoreCase));
        if (exists)
        {
            return false;
        }
        foundWord.Translations.Add(translation);
        return true;
    }

    public bool RemoveTranslation(WordDictionary dictionary, string word, string translation)
    {
        NewWord? foundWord = FindWord(dictionary, word);
        if (foundWord == null || foundWord.Translations.Count <= 1)
        {
            return false;
        }
        string? foundTranslation = foundWord.Translations.FirstOrDefault(t => t.Equals(translation, StringComparison.OrdinalIgnoreCase));

        if (foundTranslation == null)
        {
            return false;
        }
        foundWord.Translations.Remove(foundTranslation);
        return true;
    }

    public bool EditWord(WordDictionary dictionary, string oldWord, string newWord)
    {
        NewWord? foundWord = FindWord(dictionary, oldWord);
        if (foundWord == null || string.IsNullOrWhiteSpace(newWord))
        {
            return false;
        }
        if (FindWord(dictionary, newWord) != null)
        {
            return false;
        }
        foundWord.Word = newWord;
        return true;
    }

    public bool EditTranslation(WordDictionary dictionary, string word, string oldTranslation, string newTranslation)
    {
        NewWord? foundWord = FindWord(dictionary, word);
        if (foundWord == null || string.IsNullOrWhiteSpace(newTranslation))
        {
            return false;
        }
        int index = foundWord.Translations.FindIndex(t => t.Equals(oldTranslation, StringComparison.OrdinalIgnoreCase));

        if (index == -1)
        {
            return false;
        }
        
        bool duplicate = foundWord.Translations.Any(t => t.Equals(newTranslation, StringComparison.OrdinalIgnoreCase));
        if (duplicate)
        {
            return false;
        }
        foundWord.Translations[index] = newTranslation;
        return true;
    }
}