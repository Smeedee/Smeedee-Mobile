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
        private ServiceLocator locator;

        [SetUp]
        public void SetUp()
        {
           locator = new ServiceLocator(); 
        }

        [Test]
        public void Should_be_able_to_store_and_retrieve()
        {
            var instance = new Foo();
            locator.Bind<IFoo>(instance);

            var obj = locator.Get<IFoo>();

            Assert.AreSame(instance, obj);
        }

        [Test]
        public void Should_be_able_to_handle_multiple_bindings()
        {
            var ins1 = new Foo();
            var ins2 = new Bar();

            locator.Bind<IFoo>(ins1);
            locator.Bind<IBar>(ins2);

            Assert.AreSame(ins1, locator.Get<IFoo>());
            Assert.AreSame(ins2, locator.Get<IBar>());
        }
    }

    public interface IFoo { }
    public interface IBar { }
    public class Foo : IFoo { }
    public class Bar : IBar { }
}
