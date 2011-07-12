using System;

namespace Smeedee.Services
{
    public interface IPersistenceService
    {
        void Save(string key, Object value);
        T Get<T>(string key, T defaultObject);
    }
}
