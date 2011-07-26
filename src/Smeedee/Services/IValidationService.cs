using System;

namespace Smeedee.Services
{
	public interface IValidationService
	{
		void Validate(string url, string key, Action<bool> callback);
	}
}

