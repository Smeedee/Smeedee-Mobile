using System;
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
			public class TestWidget : IWidget
			{
			}
			
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
	}
}
