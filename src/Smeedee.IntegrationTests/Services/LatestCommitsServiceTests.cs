using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.IntegrationTests
{
    [TestFixture]
    public class LatestCommitsServiceTests
    {
        [Test]
        public void Should_be_able_to_fetch_and_parse_service_when_server_is_running_on_localhost()
        {
            var persistance = new FakePersistenceService();
            SmeedeeApp.Instance.ServiceLocator.Bind<IPersistenceService>(persistance);
            SmeedeeApp.Instance.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            SmeedeeApp.Instance.ServiceLocator.Bind<ITopCommittersService>(new TopCommittersService());
            persistance.Save("Login_Url", "http://localhost:1155/Smeedee");

            var model = new TopCommitters();
            model.Load(() => { });
            Assert.AreNotEqual(0, model.Committers.Count());
        }
    }
}
