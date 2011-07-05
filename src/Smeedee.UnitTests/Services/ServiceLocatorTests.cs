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

        [Test]
        public void Should_be_able_to_handle_multiple_bindings()
        {
            var locator = new ServiceLocator();
            var ins1 = new TestInterfaceImpl();
            var ins2 = new AnotherTestInterfaceImpl();

            locator.Bind<ITestInterface>(ins1);
            locator.Bind<IAnotherInterface>(ins2);

            var obj1 = locator.Get<ITestInterface>();
            var obj2 = locator.Get<IAnotherInterface>();

            Assert.True(ins1 == obj1 && ins2 == obj2);
        }
    }

    public class AnotherTestInterfaceImpl : IAnotherInterface
    {
    }

    public interface IAnotherInterface
    {
    }

    public class TestInterfaceImpl : ITestInterface
    {
    }

    public interface ITestInterface
    {
    }
}
