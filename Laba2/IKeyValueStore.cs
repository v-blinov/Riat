namespace Laba2
{
    public interface IKeyValueStore
    {
        public void Create(KeyValue keyValue);
        public KeyValue Find(string key);
        public void Update(string key, string value);
    }
}
