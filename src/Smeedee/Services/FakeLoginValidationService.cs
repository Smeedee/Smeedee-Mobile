using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Services
{
    public class FakeLoginValidationService : ILoginValidationService
    {
        public bool IsValid(string url, string key)
        {
            return url == "url" && key == "pass";
        }
    }
}
