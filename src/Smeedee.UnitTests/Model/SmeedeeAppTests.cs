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

                var widgets = app.AvailableWidgets;

                Assert.Contains(typeof(TestWidget), widgets.Select(m => m.Type).ToList());
                Assert.Contains(typeof(AnotherTestWidget), widgets.Select(m => m.Type).ToList());
            }

            [Test]
            public void Then_it_should_not_contain_interfaces()
            {
                app.RegisterAvailableWidgets();

                var widgets = app.AvailableWidgets;

                foreach (var widgetType in widgets)
                {
                    Assert.IsFalse(widgetType.Type.IsInterface);
                }
            }
            
            [Test]
            public void Registering_widgets_should_be_idempotent()
            {
                app.RegisterAvailableWidgets();
                var countOne = app.AvailableWidgets.Count;

                app.RegisterAvailableWidgets();
                var countTwo = app.AvailableWidgets.Count;

                Assert.AreEqual(countOne, countTwo);
            }

            [Test]
            public void Registered_widgets_should_have_their_name_properly_set_in_the_model()
            {
                app.RegisterAvailableWidgets();
                var widgets = app.AvailableWidgets;

                Assert.Contains("Test Widget", widgets.Select(m => m.Name).ToList());
                Assert.Contains("Test Widget 2", widgets.Select(m => m.Name).ToList());
            }

            [Test]
            public void Registered_widget_should_have_their_icon_properly_set_in_the_model()
            {
                app.RegisterAvailableWidgets();
                var widgets = app.AvailableWidgets;

                Assert.Contains(1, widgets.Select(m => m.Icon).ToList());
                Assert.Contains(2, widgets.Select(m => m.Icon).ToList());
            }

            [Test]
            public void Registered_widget_should_have_their_enabled_state_set_properly()
            {
                app.RegisterAvailableWidgets();
                var widgets = app.AvailableWidgets;

                Assert.Contains("description static 1", widgets.Select(m => m.DescriptionStatic).ToList());
                Assert.Contains("description static 2", widgets.Select(m => m.DescriptionStatic).ToList());
            }
        }
        
        public class Shared
        {
            protected SmeedeeApp app;
            
            [SetUp]
            public void SetUp()
            {
                app = SmeedeeApp.Instance;
                app.AvailableWidgets.Clear();
            }
        }
        
        [WidgetAttribute("Test Widget", 1, DescriptionStatic = "description static 1")]
        public class TestWidget : IWidget
        {
            public void Refresh()
            {
            }
        }

        [WidgetAttribute("Test Widget 2", 2, DescriptionStatic = "description static 2")]
        public class AnotherTestWidget : IWidget
        {
            public void Refresh()
            {
            }
        }
        
        public interface IIntermediateWidget : IWidget
        {
        }
    }
}
