using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Smeedee.Services
{
    public class PersistenceService : IPersistenceService
    {
        private IMobileKVPersister storage;
        
        public PersistenceService(IMobileKVPersister mobileKvStorage)
        {
            storage = mobileKvStorage;
        }

        private string Serialize(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                new BinaryFormatter().Serialize(memoryStream, obj);
                memoryStream.Position = 0;

                return Convert.ToBase64String(memoryStream.ToArray());
            }
            finally
            {
                memoryStream.Close();
            }
        }

        private T Deserialize<T>(string obj)
        {
            byte[] bytes = Convert.FromBase64String(obj);
            MemoryStream memoryStream = new MemoryStream(bytes);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var deserialized = binaryFormatter.Deserialize(memoryStream);
            if (!(deserialized is T))
                throw new ArgumentException("The stored object is not of the type you are trying to access it as.");
            return (T)deserialized;
        }

        public void Save(string key, Object value)
        {
            storage.Save(key, Serialize(value));
        }

        public T Get<T>(string key, T defaultObject)
        {
            try
            {
                return Deserialize<T>(storage.Get(key));
            }
            catch (Exception e)
            {
                throw e;
                return defaultObject;
            }
        }
    }
}
