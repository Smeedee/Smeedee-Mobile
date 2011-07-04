using System;
using System.Web.Script.Serialization;

namespace Smeedee.Services
{
    public class PersistenceService : IPersistenceService
    {
        private IMobileKVPersister storage;
        private JavaScriptSerializer jsonSerializer;
        public PersistenceService(IMobileKVPersister mobileKvStorage)
        {
            storage = mobileKvStorage;
            jsonSerializer = new JavaScriptSerializer();
        }

        public void Save(string key, Object value)
        {
            storage.Save(key, jsonSerializer.Serialize(value));
        }

        public T Get<T>(string key, T defaultObject)
        {
            return jsonSerializer.Deserialize<T>(storage.Get(key));
        }
    }
}
