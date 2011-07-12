using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Utilities;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Working Days Left", Resource.Drawable.icon_workingdaysleft, DescriptionStatic = "Actual working days left of project")]
    public class WorkingDaysLeftWidget : RelativeLayout, IWidget
    {
        private readonly IModelService<WorkingDaysLeft> modelService =
            SmeedeeApp.Instance.ServiceLocator.Get<IModelService<WorkingDaysLeft>>();
        
        private readonly IBackgroundWorker backgroundWorker = 
            SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        private WorkingDaysLeft model;

        public WorkingDaysLeftWidget(Context context) : base(context)
        {
            InflateView();
            backgroundWorker.Invoke(LoadModelAndUpdateView);
        }

        private void InflateView()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null)
            {
                inflater.Inflate(Resource.Layout.WorkingDaysLeftWidget, this);
            } else
            {
                throw new Exception("Unable to inflate view on Working days left widget");
            }
        }

        private void LoadModelAndUpdateView()
        {
            LoadModel();
            ((Activity)Context).RunOnUiThread(UpdateView);
        }

        private void LoadModel()
        {
            var args = new Dictionary<string, string>();
            model = modelService.GetSingle(args);
        }

        private void UpdateView()
        {
            var daysView = FindViewById<TextView>(Resource.Id.WorkingDaysLeftNumber);
            var textView = FindViewById<TextView>(Resource.Id.WorkingDaysLeftText);
            
            var days = model.DaysLeft.ToString();
            var text = model.DaysLeftText;

            daysView.SetText(days, TextView.BufferType.Normal);
            textView.SetText(text, TextView.BufferType.Normal);
        }

        public void Refresh()
        {
            LoadModelAndUpdateView();
        }
    }
}