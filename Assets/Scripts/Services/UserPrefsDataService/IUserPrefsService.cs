namespace Services.UserPrefsDataService
{
    public interface IUserPrefsService
    {
        bool SaveData<T>(string key, T data, bool encrypted = true);
        
        T LoadData<T>(string key, bool encrypted = true);
        
        bool ExistsData(string key);
    }
}
