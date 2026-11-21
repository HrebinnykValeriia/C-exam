namespace exam.Service;

using exam.Interfaces;
using exam.Models;

public class UserService : IUserService
{
    private readonly List<User> _users;
    private User? _user;

    public UserService(List<User> users)
    {
        _users = users ?? new List<User>();
    }
    public void LoadUsers(List<User> users)
    {
        _users.Clear();
        _users.AddRange(users);
    }
    public User CreateUser(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username)) 
            throw new ArgumentException("Username cannot be empty.");
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.");
        
        if (FindUser(username) != null)
        {
            throw new ArgumentException("User already exists.");
        }
        _user = new User(username, password);
        _users.Add(_user);
        return _user;
    }
    
    public bool CheckPassword(string username, string password)
    {
        User? user = FindUser(username);
        if (user == null)
            return false;

        return user.Password == password;
    }
    public void Counter(string dictTitle)
    {
        if (string.IsNullOrWhiteSpace(dictTitle) || _user == null)
        {
            return;
        }

        if (_user.DictionaryUsage.ContainsKey(dictTitle))
        {
            _user.DictionaryUsage[dictTitle]++;
        }
        else
        {
            _user.DictionaryUsage.Add(dictTitle, 1);
        }
    }

    public Dictionary<string, int> GetCounter()
    {
        if (_user == null)
        {
            return new Dictionary<string, int>();
        }
        return _user.DictionaryUsage; 
    }

    public User? FindUser(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return null;
        }
        return _users.Find(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
    
    public bool RememberUser(string username)
    {
        User? foundUser = FindUser(username);

        if (foundUser == null)
        {
            return false;
        }

        _user = foundUser;
        return true;
    }
    public User? GetCurrentUser()
    {
        return _user;
    }
    
}