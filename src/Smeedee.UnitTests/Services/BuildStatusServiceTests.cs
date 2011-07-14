using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class BuildStatusServiceTests
    {
        [Test]
        public void Should_implement_IModelService()
        {
            Assert.True(typeof(IModelService<BuildStatusBoard>).IsAssignableFrom(typeof(FakeBuildStatusService)));
        }
    }
}
