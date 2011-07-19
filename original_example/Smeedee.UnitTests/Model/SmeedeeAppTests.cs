using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Model
{
    public class SmeedeeAppTests
    {
        [TestFixture]
        public class When_in_default_state
        {
            [Test]
            public void Then_the_default_service_should_be_the_HTTP_service()
            {
                var httpServiceType = typeof(SmeedeeHttpService);
                var defaultServiceType = SmeedeeApp.SmeedeeService.GetType();
                
                Assert.AreEqual(httpServiceType.Name, defaultServiceType.Name);
            }
        }
        
        [TestFixture]
        public class When_registering_widgets_dynamically : Shared
        {
            [Test]
            public void Then_they_should_be_available_when_getting_widgets()
            {
                app.RegisterAvailableWidgets();

                var widgets = app.AvailableWidgetTypes;

                Assert.Contains(typeof(TestWidget), widgets.ToList());
                Assert.Contains(typeof(AnotherTestWidget), widgets.ToList());
            }
            
            [Test]
            public void Then_it_should_not_contain_interfaces()
            {
                app.RegisterAvailableWidgets();

                var widgetTypes = app.AvailableWidgetTypes;
                
                foreach (var widgetType in widgetTypes)
                {
                    Assert.IsFalse(widgetType.IsInterface);
                }
            }
            
            [Test]
            public void Registering_widgets_should_be_idempotent()
            {
                app.RegisterAvailableWidgets();
                var countOne = app.AvailableWidgetTypes.Count;

                app.RegisterAvailableWidgets();
                var countTwo = app.AvailableWidgetTypes.Count;

                Assert.AreEqual(countOne, countTwo);
            }
        }
        
        public class Shared
        {
            protected SmeedeeApp app;
            
            [SetUp]
            public void SetUp()
            {
                app = SmeedeeApp.Instance;
                app.AvailableWidgetTypes.Clear();
            }
        }
        
        public class TestWidget : IWidget
        {
        }
        
        public class AnotherTestWidget : IWidget
        {
        }
        
        public interface IIntermediateWidget : IWidget
        {
        }
    }
}
