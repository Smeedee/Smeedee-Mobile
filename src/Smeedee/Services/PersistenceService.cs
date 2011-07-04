using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Smeedee.Services
{
    public class PersistenceService : IPersistenceService
    {
        private IMobileKVPersister storage;
        public PersistenceService(IMobileKVPersister mobileKvStorage)
        {
            storage = mobileKvStorage;
        }

        public void Save(string key, Object value)
        {
            storage.Save(key, value.ToString());
        }

        public T Get<T>(string key, T defaultObject)
        {
            throw new NotImplementedException();
        }
    }
}
