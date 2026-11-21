namespace exam.Interfaces;
using exam.Models;
public interface IUserFileService
{
    void SaveUser(User user, string path);
    User? LoadUser(string path);
    List<User> LoadUsers(string directory);
}