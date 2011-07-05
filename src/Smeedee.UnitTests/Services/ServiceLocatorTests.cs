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
        public void Should_be_able_to_store_and_retrieve()
        {
            var locator = new ServiceLocator();
            var instance = new TestInterfaceImpl();
            locator.Bind<ITestInterface>(instance);

            var obj = locator.Get<ITestInterface>();

            Assert.AreSame(instance, obj);
        }
    }

    public class TestInterfaceImpl : ITestInterface
    {
    }

    public interface ITestInterface
    {
    }
}
