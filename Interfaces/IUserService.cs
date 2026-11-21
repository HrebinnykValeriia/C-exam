namespace exam.Interfaces;
using exam.Models;
public interface IUserService
{
    User CreateUser(string username, string password);
    bool CheckPassword(string username, string password);
    void Counter(string DictTitle);
    Dictionary<string, int>  GetCounter();
    User? FindUser(string username);
    bool RememberUser(string username);
    void LoadUsers(List<User> users);
    User? GetCurrentUser();
}