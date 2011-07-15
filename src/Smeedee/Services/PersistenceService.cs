using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Smeedee
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
            storage.Save(key, Serialize(value));
        }

        public T Get<T>(string key, T defaultObject)
        {
            try
            {
                return Deserialize<T>(storage.Get(key));
            }
			catch (ArgumentNullException)
			{
				//storage returned null, we want to return the default object
				return defaultObject;
			}
            catch (ArgumentException)
            {
				//we could not deserialize the string, we want to know about this 
                throw;
            }
            catch (Exception)
            {
                return defaultObject;
            }
        }

        private static string Serialize(object obj)
        {
            var memoryStream = new MemoryStream();
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

        private static T Deserialize<T>(string obj)
        {
            byte[] bytes = Convert.FromBase64String(obj);
            var binaryFormatter = new BinaryFormatter();
            var deserialized = binaryFormatter.Deserialize(new MemoryStream(bytes));
            if (!(deserialized is T))
                throw new ArgumentException("The stored object is of type " + deserialized.GetType() + ", which can't be cast to " + typeof(T));
            return (T)deserialized;
        }
    }
}
