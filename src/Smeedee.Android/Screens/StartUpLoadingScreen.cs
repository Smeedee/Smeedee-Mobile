using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Java.Lang;
using Smeedee.Model;
using Smeedee.Services;
using Exception = System.Exception;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Loading", Theme = "@android:style/Theme.NoTitleBar")]
    public class StartUpLoadingScreen : Activity
    {
        private ILog logger;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.StartUpLoadingScreen);

            logger = SmeedeeApp.Instance.ServiceLocator.Get<ILog>();
            
            WidgetContainer.SetWidgets(GetWidgets());
            var widgetContainer = new Intent(this, typeof(WidgetContainer));
            StartActivity(widgetContainer);
            Finish();
        }
        private IEnumerable<IWidget> GetWidgets()
        {
            SmeedeeApp.Instance.RegisterAvailableWidgets();

            var availableWidgets = SmeedeeApp.Instance.AvailableWidgets;
            var instances = new List<IWidget>();
            foreach (var widget in availableWidgets)
            {
                try
                {
                    logger.Log("SMEEDEE", "Instantiating widget of type: " + widget.Type.Name);
                    instances.Add(Activator.CreateInstance(widget.Type, this) as IWidget);
                }
                catch (Exception e)
                {
                    logger.Log("SMEEDEE", Throwable.FromException(e).ToString(), "Exception thrown when instatiating widget");
                }
            }
            return instances;
        }

    }
}