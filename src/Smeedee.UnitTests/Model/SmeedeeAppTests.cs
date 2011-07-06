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
