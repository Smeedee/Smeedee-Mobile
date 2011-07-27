using System;

namespace Smeedee.Services.Fakes
{
	public class FakeValidationService : IValidationService
	{
		public FakeValidationService ()
		{
		}
		public void Validate(string url, string key, Action<bool> callback) 
		{
			callback( true /*(url == "http://folk.ntnu.no/dagolap/s/" && key == "pass")*/);
		}
		
	}
}
