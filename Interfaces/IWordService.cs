namespace exam.Interfaces;
using exam.Service;
using exam.Models;
public interface IWordService
{
    bool AddWord(WordDictionary dictionary, NewWord word);
    bool RemoveWord(WordDictionary dictionary, NewWord word);
    NewWord? FindWord(WordDictionary dictionary, string word);
    bool AddTranslation(WordDictionary dictionary, string word, string translation);
    bool RemoveTranslation(WordDictionary dictionary, string word, string translation);
    bool EditWord(WordDictionary dictionary, string oldWord, string newWord);
    bool EditTranslation(WordDictionary dictionary, string word, string oldTranslation, string newTranslation);
}