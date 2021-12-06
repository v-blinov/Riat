namespace Laba2
{
    public class KeyValueStore : HttpClientBase, IKeyValueStore
    {
        private const string DefaultHostUrl = "https://tolltech.ru/study/";

        public KeyValueStore(string hostUrl = DefaultHostUrl) : base(hostUrl)
        { }

        public void Create(KeyValue keyValue)
        {
            MakePostRequest("Create", keyValue);
        }

        public KeyValue Find(string key)
        {
            return MakeGetRequest<KeyValue>("Find", (Key: "key", Value: key));
        }

        public void Update(string key, string value)
        {
            var urlParameters = new[]
            {
                (Key: "key", Value: key),
                (Key: "value", Value: value)
            };

            MakePostRequest("Update", (object)null, urlParameters);
        }
    }
}
