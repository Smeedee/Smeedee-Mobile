﻿using System;
using System.Collections.Generic;

namespace Smeedee.Services
{
    public class ServiceLocator
    {
        private readonly IDictionary<Type, object> store;

        public ServiceLocator()
        {
            store = new Dictionary<Type, object>();
        }

        public void Bind<T>(T arg)
        {
            store[typeof(T)] = arg;
        }

        public T Get<T>()
        {
            if (store.ContainsKey(typeof(T)))
            {
                return (T)store[typeof(T)];
            }
            throw new ArgumentException("Type not bound: "+typeof(T).Name);
        }
    }
}
