﻿namespace Smeedee.Services
{
    public class FakeLoginValidationService : ILoginValidationService
    {
        public bool IsValid(string url, string key)
        {
            return true;
        }
    }
}
