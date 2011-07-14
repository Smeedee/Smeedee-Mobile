using NUnit.Framework;
using Smeedee.Model;
using Smeedee;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class BuildStatusServiceTests
    {
        [Test]
        public void Should_implement_IModelService()
        {
            Assert.True(typeof(IModelService<BuildStatus>).IsAssignableFrom(typeof(FakeBuildStatusService)));
        }
    }
}
