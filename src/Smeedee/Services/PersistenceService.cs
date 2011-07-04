using System;
using System.Collections.Generic;
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
            // TODO: Is it correct behavior to allow all other exceptions to bubble up?
            // TODO: For now we have a bit of duplication here to allow tests to prove the
            // system behavior. If logging is ever implemented that would be a better way
            // to assert system behavior.
            try
            {
                return jsonSerializer.Deserialize<T>(storage.Get(key));
            }
            catch(KeyNotFoundException)
            {
                return defaultObject;
            }
            catch(FormatException)
            {
                return defaultObject;
            }
        }
    }
}
