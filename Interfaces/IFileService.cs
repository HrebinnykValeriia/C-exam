namespace exam.Interfaces;
using exam.Models;
using exam.DTO;
public interface IFileService
{
    void SaveDictionary(string path, WordDictionary dictionary);
    WordDictionary? LoadDictionary(string path);
    void SaveDictionaryList(List<DictionaryInfo> dictionaryList, string path);
    List<DictionaryInfo> LoadDictionaryList(string path);
    void DeleteFile(string path);
    void ExportWord(string path, NewWord  word);
   
}