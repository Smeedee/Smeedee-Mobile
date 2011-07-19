using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Services
{
    public interface IMobileKVPersister
    {
        void Save(string key, string value);
        string Get(string key);
    }
}
