using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Smeedee.Services
{
    public interface IPersistenceService
    {
        void Save(string key, Object value);
        T Get<T>(string key, T defaultObject);
    }
}
