using System;
using NUnit.Framework;
using Smeedee;

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
            var foo = new Foo();
            var bar = new Bar();

            locator.Bind<IFoo>(foo);
            locator.Bind<IBar>(bar);

            Assert.AreSame(foo, locator.Get<IFoo>());
            Assert.AreSame(bar, locator.Get<IBar>());
        }

        [Test]
        public void Should_be_able_to_overwrite_bindings()
        {
            var firstInstance = new Foo();
            var secondInstance = new Foo();

            locator.Bind<IFoo>(firstInstance);
            locator.Bind<IFoo>(secondInstance);

            Assert.AreSame(secondInstance, locator.Get<IFoo>());
        }

        [Test]
        [ExpectedException (typeof(ArgumentException))]
        public void Should_throw_exception_when_asking_for_unbound_types()
        {
            locator.Get<IFoo>();
        }
    }

    public interface IFoo { }
    public interface IBar { }
    public class Foo : IFoo { }
    public class Bar : IBar { }
}
