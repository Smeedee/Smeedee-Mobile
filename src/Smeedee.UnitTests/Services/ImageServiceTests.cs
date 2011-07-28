using System;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests
{
	// TODO: Move this to integration test. Dont have that in iOS project
	
	[TestFixture]
	public class ImageServiceTests
	{
		private IBackgroundWorker worker;
		
		[SetUp]
		public void SetUp()
		{
			worker = new NoBackgroundInvocation();
		}
		
		[Test]
		public void Should_load_byte_array_of_google_logo()
		{
			var uri = new Uri("http://www.google.com/images/logo.png");
			var service = new ImageService(worker);
			
			var empty = true;
			service.GetImage(uri, (bytes) => {
				empty = bytes.Length == 0;
			});
			
			Assert.IsFalse(empty);
		}
		
		[Test]
		public void Should_return_null_on_invalid_uri()
		{
			var uri = new Uri("http://obvoiusly.invalid.com/not/a/picture.png");
			var service = new ImageService(worker);
			
			var wasNull = false;
			service.GetImage(uri, (bytes) => {
				wasNull = bytes == null;
			});
			
			Assert.IsTrue(wasNull);
		}
	}
}

