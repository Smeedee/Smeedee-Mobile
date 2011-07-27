using System;
using System.Collections.Generic;

namespace Smeedee.Services
{
    public class ServiceLocator
    {
        private readonly IDictionary<string, object> store;

        public ServiceLocator()
        {
            store = new Dictionary<string, object>();
        }

        public void Bind<T>(T arg)
        {
            store[typeof(T).FullName] = arg;
        }

        public T Get<T>()
        {
            if (store.ContainsKey(typeof(T).FullName))
            {
                return (T)store[typeof(T).FullName];
            }
            throw new ArgumentException(string.Format("Type {0} not bound", typeof(T).FullName));
        }
    }
}
