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
		public class When_registering_widgets : Shared
		{
			[Test]
			public void Then_they_should_be_available_when_getting_widgets()
			{
				app.RegisterAvailableWidgets(new [] { 
					typeof(TestWidget)
				});
				
				var widgets = app.GetWidgets();
				
				Assert.AreEqual(1, widgets.Count());
				Assert.AreEqual(typeof(TestWidget).Name, widgets.First().GetType().Name);
			}
			
			[Test]
			public void Then_a_widget_already_registered_should_not_appear_twice()
			{
				app.RegisterAvailableWidgets(new [] { 
					typeof(TestWidget),
					typeof(TestWidget)
				});
				
				var widgets = app.GetWidgets();
				
				Assert.AreEqual(1, widgets.Count());
			}
		}
		
		[TestFixture]
		public class When_registering_widgets_dynamically : Shared
		{
			[Test]
			public void Then_they_should_be_available_when_getting_widgets()
			{
				app.RegisterAvailableWidgets();
				
				var widgets = app.GetWidgets();
				
				AssertContains(widgets, typeof(TestWidget));
				AssertContains(widgets, typeof(AnotherTestWidget));
			}
			
			[Test]
			public void Then_it_should_only_contain_concrete_types()
			{
				app.RegisterAvailableWidgets();

                var exceptionWasThrown = false;
                var widgets = new List<IWidget>();
				try {
					widgets = app.GetWidgets().ToList();
				} catch {
					exceptionWasThrown = true;
				}
			    var nonConcrete = widgets.Where(o => o.GetType().IsInterface || o.GetType().IsAbstract).ToList();
                Assert.IsEmpty(nonConcrete);
				Assert.IsFalse(exceptionWasThrown);
			}
			
			private void AssertContains(IEnumerable<IWidget> allWidgets, Type typeToCheck)
			{
				var testWidget = allWidgets.Single(w => w.GetType() == typeToCheck);
				Assert.IsNotNull(testWidget);
			}
		}
		
		public class Shared
		{
			protected SmeedeeApp app;
			
			[SetUp]
			public void SetUp()
			{
				app = SmeedeeApp.Instance;
				app.ClearRegisteredWidgets();
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
