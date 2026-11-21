namespace exam.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public Dictionary<string, int> DictionaryUsage { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        DictionaryUsage = new Dictionary<string, int>();
    }
    // public User()
    // {
    //     Username = string.Empty;
    //     DictionaryUsage = new Dictionary<string, int>();
    // }
    public override string ToString()
    {
        return $"{Username}: {string.Join(", ", DictionaryUsage.Keys)}";
    }
}