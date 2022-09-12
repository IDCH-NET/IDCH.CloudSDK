namespace IDCH.StorageBrowser.Data
{
    public interface ISessionStorage
    {
        //event EventHandler<ChangingEventArgs> Changing;
        //event EventHandler<ChangedEventArgs> Changed;

        void Clear();
        bool ContainKey(string key);
        T GetItem<T>(string key);
        string GetItemAsString(string key);
        string Key(int index);
        int Length();
        void RemoveItem(string key);
        void SetItem<T>(string key, T data);
        void SetItemAsString(string key, string data);
    }
}
