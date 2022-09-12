namespace IDCH.StorageBrowser.Data
{
    public class LocalMemoryStorageService : ISessionStorage
    {
        private Dictionary<string, object> Datas { get; set; }
        public LocalMemoryStorageService()
        {
            Datas = new();
        }
        public void Clear()
        {
            Datas.Clear();
        }

        public bool ContainKey(string key)
        {
            return Datas.ContainsKey(key);
        }

        public T GetItem<T>(string key)
        {
            return Datas.ContainsKey(key) ? (T)Datas[key] : default(T);
        }

        public string GetItemAsString(string key)
        {
            return Datas.ContainsKey(key) ? Datas[key].ToString() : string.Empty;
        }

        public string Key(int index)
        {
            var keys = Datas.Keys.ToList();
            return keys[index];
        }

        public int Length()
        {
            return Datas.Count;
        }

        public void RemoveItem(string key)
        {
            if (Datas.ContainsKey(key))
            {
                Datas.Remove(key);
            }
        }

        public void SetItem<T>(string key, T data)
        {
            if (Datas.ContainsKey(key))
            {
                Datas[key] = data;
            }
            else
            {
                Datas.Add(key, data);
            }
        }

        public void SetItemAsString(string key, string data)
        {
            if (Datas.ContainsKey(key))
            {
                Datas[key] = data;
            }
            else
            {
                Datas.Add(key, data);
            }
        }
    }
}
