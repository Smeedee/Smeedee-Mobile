using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Assert.True(typeof(IModelService<BuildStatus>).IsAssignableFrom(typeof(BuildStatusService)));
        }
    }
}
