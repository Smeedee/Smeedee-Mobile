using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Services;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class ServiceLocatorTests
    {
        [Test]
        public void Should_be_able_to_bind_class_implementing_the_said_interface()
        {
            var locator = new ServiceLocator();
            locator.Bind<ITestInterface>(new TestInterfaceImpl());
        }
    }

    public class TestInterfaceImpl : ITestInterface
    {
    }

    public interface ITestInterface
    {
    }
}
