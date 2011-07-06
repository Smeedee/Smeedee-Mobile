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
        public class When_registering_widgets_dynamically : Shared
        {
            [Test]
            public void Then_their_type_should_be_contained_in_a_returned_model()
            {
                app.RegisterAvailableWidgets();

                var widgets = app.AvailableWidgetTypes;

                Assert.Contains(typeof(TestWidget), widgets.Select(m => m.Type).ToList());
                Assert.Contains(typeof(AnotherTestWidget), widgets.Select(m => m.Type).ToList());
            }


            [Test]
            public void Then_it_should_not_contain_interfaces()
            {
                app.RegisterAvailableWidgets();

                var widgetTypes = app.AvailableWidgetTypes;

                foreach (var widgetType in widgetTypes)
                {
                    Assert.IsFalse(widgetType.Type.IsInterface);
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

            [Test]
            public void Registered_widgets_should_have_their_name_properly_set_in_the_model()
            {
                app.RegisterAvailableWidgets();
                var widgets = app.AvailableWidgetTypes;

                Assert.Contains("Test Widget", widgets.Select(m => m.Name).ToList());
                Assert.Contains("Test Widget 2", widgets.Select(m => m.Name).ToList());
            }

            [Test]
            public void Registered_widget_should_have_their_icon_properly_set_in_the_model()
            {
                app.RegisterAvailableWidgets();
                var widgets = app.AvailableWidgetTypes;

                Assert.Contains("icon/url", widgets.Select(m => m.Icon).ToList());
                Assert.Contains("icon/url/2", widgets.Select(m => m.Icon).ToList());
            }

            [Test]
            [Ignore]
            public void Registered_widget_should_have_their_enabled_state_set_properly()
            {
                // TODO: Make up something smart.
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
        
        [WidgetAttribute("Test Widget", "icon/url")]
        public class TestWidget : IWidget
        {
        }

        [WidgetAttribute("Test Widget 2", "icon/url/2")]
        public class AnotherTestWidget : IWidget
        {
        }
        
        public interface IIntermediateWidget : IWidget
        {
        }
    }
}
