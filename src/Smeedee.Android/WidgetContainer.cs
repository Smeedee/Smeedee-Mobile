using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Smeedee.Android.Widgets;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class WidgetContainer : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ConfigureDependencies();

            SetContentView(Resource.Layout.Main);

            var flipper = FindViewById<ViewFlipper>(Resource.Id.Flipper);

            var widgets = GetWidgets();
            foreach (var widget in widgets)
            {
                flipper.AddView(widget as View);
            }

            var text = FindViewById<Button>(Resource.Id.BtnNext);
            text.Text = widgets.Count() + " w";

            var btnPrev = FindViewById<Button>(Resource.Id.BtnPrev);
            btnPrev.Click += (obj, e) =>
                                 {
                                     flipper.ShowPrevious();
                                     flipper.RefreshDrawableState();
                                 };

            var btnNext = FindViewById<Button>(Resource.Id.BtnNext);
            btnNext.Click += delegate
                                 {
                                     flipper.ShowNext();
                                 };
        }

        private void ConfigureDependencies()
        {
            SmeedeeApp.SmeedeeService = new SmeedeeFakeService();
        }

        private IEnumerable<IWidget> GetWidgets()
        {
            SmeedeeApp.Instance.RegisterAvailableWidgets();

            var widgetTypes = SmeedeeApp.Instance.AvailableWidgetTypes;
            var instances = new List<IWidget>();
            foreach (var widgetType in widgetTypes)
            {
                instances.Add(Activator.CreateInstance(widgetType, this) as IWidget);
            }
            return instances;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return true;
        }
    }
}

