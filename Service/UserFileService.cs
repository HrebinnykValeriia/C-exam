using exam.Models;
using System.Text.Json;

namespace exam.Service;

using exam.Interfaces;

public class UserFileService : IUserFileService
{
    public void SaveUser(User user, string path)
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
        string json = JsonSerializer.Serialize(user, options);
        File.WriteAllText(path, json);
    }

    public User? LoadUser(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<User>(json);
    }

    public List<User> LoadUsers(string directory)
    {
        if (!Directory.Exists(directory))
        {
            return new List<User>();
        }
        List<User> usersList = new List<User>();
        foreach (var file in Directory.EnumerateFiles(directory,"*.json"))
        {
            User? user = LoadUser(file);
            if (user != null)
            {
                usersList.Add(user);
            }
            
        }
        return usersList;
        
    }
}