namespace exam.Service;
using exam.Models;
using exam.Interfaces;
using exam.DTO;
using Serilog;
public class Manager
{
    private readonly IFileService _fileService;
    private readonly IDictionaryService _dictionaryService;
    private readonly IWordService _wordService;
    private readonly List<DictionaryInfo> _dictionaryInfos = new();
    private readonly string _dictionaryListPath = "../../../Data/dictionaries.txt";
    private readonly IUserService  _userService;
    private readonly IUserFileService _userFileService;
    private readonly string _usersDirectory = "../../../Data/Users";
    
    public Manager(IFileService fileService, IDictionaryService dictionaryService, IWordService wordService, 
        IUserService userService, IUserFileService userFileService)
    {
        _fileService = fileService;
        _dictionaryService = dictionaryService;
        _wordService = wordService;
        _userService = userService;
        _userFileService = userFileService;
    }
    
    public IReadOnlyList<WordDictionary> GetDictionaries()
    {
        return _dictionaryService.Dictionaries;
    }
    public WordDictionary? FindDictionary(string title)
    {
        return _dictionaryService.FindDictionary(title);
    } 
    public NewWord? FindWord(WordDictionary dictionary, string word)
    {
        return _wordService.FindWord(dictionary, word);
    }
    public void LoadDictionaries()
    {
        _dictionaryInfos.Clear();
        _dictionaryService.ClearDictionaries();

        List<DictionaryInfo> list = _fileService.LoadDictionaryList(_dictionaryListPath);
        foreach (DictionaryInfo info in list)
        {
            _dictionaryInfos.Add(info);
        }
        foreach (DictionaryInfo info in _dictionaryInfos)
        {
            WordDictionary? dictionary = _fileService.LoadDictionary(info.FilePath);
            if (dictionary != null)
            {
                _dictionaryService.AddDictionary(dictionary);
            }
        }
    }
    public void SaveDictionaries()
    {
        foreach (WordDictionary dictionary in _dictionaryService.Dictionaries)
        {
            DictionaryInfo? dictionaryInfo = _dictionaryInfos.FirstOrDefault(d =>
                d.Name.Equals(dictionary.Title, StringComparison.OrdinalIgnoreCase));

            if (dictionaryInfo != null)
            {
                _fileService.SaveDictionary(dictionaryInfo.FilePath, dictionary);
            }
        }
        _fileService.SaveDictionaryList(_dictionaryInfos, _dictionaryListPath);
    }

    public void CreateDictionary(WordDictionary dictionary, string path)
    {
        if (_dictionaryService.FindDictionary(dictionary.Title) != null)
        {
            return;
        }
        bool added = _dictionaryService.AddDictionary(dictionary);
        if (!added)
        {
            return;
        }
        _fileService.SaveDictionary(path, dictionary);
        _dictionaryInfos.Add(new DictionaryInfo(dictionary.Title, path));
        _fileService.SaveDictionaryList(_dictionaryInfos, _dictionaryListPath);
        Log.Information("Dictionary {DictionaryTitle} created", dictionary.Title);
    }

    public bool DeleteDictionary(string title)
    {
        DictionaryInfo? dictionaryInfo = _dictionaryInfos.FirstOrDefault(d => 
            d.Name.Equals(title, StringComparison.OrdinalIgnoreCase));
        
        if (dictionaryInfo == null)
        {
            Log.Warning("Attempted to delete dictionary {DictionaryTitle}, but it was not found", title);
            return false;
        }
        bool removed = _dictionaryService.RemoveDictionary(title);
        if (!removed)
        {
            return false;
        }
        _fileService.DeleteFile(dictionaryInfo.FilePath);
        _dictionaryInfos.Remove(dictionaryInfo);
        _fileService.SaveDictionaryList(_dictionaryInfos, _dictionaryListPath);
        Log.Information("Dictionary deleted: {DictionaryTitle}", title);
        return true;
    }
    
    
    
    public bool AddWord(WordDictionary dictionary, NewWord word)
    {
        bool add = _wordService.AddWord(dictionary, word);
        if (add)
        {
            SaveDictionaries();
            Log.Information("Word {Word} added to dictionary {DictionaryTitle}", 
                word.Word, dictionary.Title);
        }
        else
        {
            Log.Warning("Failed to add word {Word} to dictionary {DictionaryTitle}", 
                word.Word, dictionary.Title);
        }
        return add;
    }
    public bool RemoveWord(WordDictionary dictionary, NewWord word)
    {
        bool rem = _wordService.RemoveWord(dictionary, word);
        if (rem)
        {
            SaveDictionaries();
            Log.Information("Word {Word} removed from dictionary {DictionaryTitle}", 
                word.Word, dictionary.Title);
        }
        else
        {
            Log.Warning("Failed to remove word {Word} from dictionary {DictionaryTitle}",
                word.Word, dictionary.Title);
        }
        return rem;
    }
    
    public bool AddTranslation(WordDictionary dictionary, string word, string translation)
    {
        bool add = _wordService.AddTranslation(dictionary, word, translation);
        if (add)
        {
            SaveDictionaries();
            Log.Information("Translation {Translation} added for word {Word} in dictionary {DictionaryTitle}",
            translation, word, dictionary.Title);
        }
        else
        {
            Log.Warning( "Failed to add translation {Translation} for word {Word} in dictionary {DictionaryTitle}",
                translation, word, dictionary.Title);
        }
        return add;
    }
    public bool RemoveTranslation(WordDictionary dictionary, string word, string translation)
    {
        bool rem =  _wordService.RemoveTranslation(dictionary, word, translation);
        if (rem)
        {
            SaveDictionaries();
            Log.Information("Translation {Translation} removed from word {Word} in dictionary {DictionaryTitle}",
                translation, word, dictionary.Title);
        }
        else
        {
            Log.Warning("Failed to remove translation {Translation} from word {Word} in dictionary {DictionaryTitle}",
                translation, word, dictionary.Title);
        }
        return rem;
    }

    public bool EditWord(WordDictionary dictionary, string oldWord, string newWord)
    {
        bool ed = _wordService.EditWord(dictionary, oldWord, newWord);
        if (ed)
        {
            SaveDictionaries();
            Log.Information("Word {OldWord} changed to {NewWord} in dictionary {DictionaryTitle}",
                oldWord, newWord, dictionary.Title);
        }
        else
        {
            Log.Warning("Failed to edit word {Word} to dictionary {DictionaryTitle}",
                oldWord, dictionary.Title);
        }
        return ed;
    }
    public bool EditTranslation(WordDictionary dictionary, string word, string oldTranslation, string newTranslation)
    {
        bool ed = _wordService.EditTranslation(dictionary, word, oldTranslation, newTranslation);
        if (ed)
        {
            SaveDictionaries();
            Log.Information("Translation {OldTranslation} changed to {NewTranslation} for word {Word} in dictionary {DictionaryTitle}",
                oldTranslation, newTranslation, word, dictionary.Title);
        }
        else
        {
            Log.Warning("Failed to edit translation {Word} to dictionary {DictionaryTitle}",
                oldTranslation, dictionary.Title);
        }
        return ed;
    }
    public void ExportWord(NewWord word, string path)
    {
        _fileService.ExportWord(path, word);
        Log.Information("Word {Word} exported to the file {FilePath}",
            word.Word, path);
    }
    public void LoadUsers()
    {
        List<User> users = _userFileService.LoadUsers(_usersDirectory);
        _userService.LoadUsers(users);
    }
    public void SaveUser(User user)
    {
        string fileName = $"{user.Username}.json";
        string path = Path.Combine(_usersDirectory, fileName);
        _userFileService.SaveUser(user, path);
    }
    public User? FindUser(string username)
    {
        return _userService.FindUser(username);
    }
    public bool CheckPassword(string username, string password)
    {
        return _userService.CheckPassword(username, password);
    }
    public User CreateUser(string username, string password)
    {
        User user = _userService.CreateUser(username, password);
        SaveUser(user);
        Log.Information("User {Username} created", username);
        return user;
    }
    public bool RememberUser(string username)
    {
        bool remember = _userService.RememberUser(username);
        if (remember)
        {
            Log.Information("User {Username} logged in", username);
            return true;
        }
        else
        {
            Log.Warning("Failed to remember user {Username}", username);
            return false;
        }
    }
    public void CounterPlus(string dictTitle)
    {
        _userService.Counter(dictTitle);
        User? user = _userService.GetCurrentUser();
        if (user != null)
        {
            SaveUser(user);
        }
    }
    public Dictionary<string, int> GetCounter()
    {
        return _userService.GetCounter();
    }
}