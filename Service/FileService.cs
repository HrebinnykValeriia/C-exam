using System.Text.Json;
using exam.Models;
using exam.Interfaces;
using exam.DTO;
using System.Text.Encodings.Web;
namespace exam.Service;

public class FileService : IFileService
{
    public void SaveDictionary(string path, WordDictionary dictionary)
    {
        string? directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

        };
        string json = JsonSerializer.Serialize(dictionary, options);
        File.WriteAllText(path, json);
    }

    public WordDictionary? LoadDictionary(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<WordDictionary>(json);
    }

    public void SaveDictionaryList(List<DictionaryInfo> dictionaryList, string path)
    {
        string? directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        List<string> lines = new List<string>
        {
            dictionaryList.Count.ToString()
        };

        foreach (DictionaryInfo info in dictionaryList)
        {
            lines.Add($"{info.Name}---{info.FilePath}");
        }
        File.WriteAllLines(path, lines);
    }

    public List<DictionaryInfo> LoadDictionaryList(string path)
    {
        if (!File.Exists(path))
        {
            return new List<DictionaryInfo>();
        }
        string[] lines = File.ReadAllLines(path);
        List<DictionaryInfo> dictionaryList = new List<DictionaryInfo>();
        if (lines.Length == 0 || !int.TryParse(lines[0], out int count))
        {
            return dictionaryList;
        }

        for (int i = 1; i < lines.Length && dictionaryList.Count < count; i++)
        {
            string[] parts = lines[i].Split("---", 2);
            if (parts.Length == 2)
            {
                dictionaryList.Add(new DictionaryInfo(parts[0], parts[1]));
            }
        }
        return dictionaryList;
    }
    public void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
    public void ExportWord(string path, NewWord word)
    {
        string? directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        List<string> lines = new List<string>
        {
            $"Word: {word.Word}", "Translations:"
        };
        foreach (string translation in word.Translations)
        {
            lines.Add($" {translation}");
        }
        File.WriteAllLines(path, lines);
    }
}