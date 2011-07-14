using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Working Days Left", StaticDescription = "Actually working days left")]
    public class WorkingDaysLeftWidget : RelativeLayout, IWidget
    {
        private readonly IModelService<WorkingDaysLeft> modelService =
            SmeedeeApp.Instance.ServiceLocator.Get<IModelService<WorkingDaysLeft>>();
        
        private readonly IBackgroundWorker backgroundWorker = 
            SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        private WorkingDaysLeft model;
        private string _dynamicDescription;

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
            model = modelService.Get();
        }

        private void UpdateView()
        {
            var daysView = FindViewById<TextView>(Resource.Id.WorkingDaysLeftNumber);
            var textView = FindViewById<TextView>(Resource.Id.WorkingDaysLeftText);
            
            var days = model.DaysLeft.ToString();
            var text = model.DaysLeftText;
            var untillDate = model.UntillDate;

            daysView.Text = days;
            textView.Text = text;
            if (model.IsOnOvertime)
                _dynamicDescription = "You should have been finished by " + untillDate.DayOfWeek.ToString() + " " +
                                      untillDate.Date.ToShortDateString();
            
            else
                _dynamicDescription = "Working days left untill " + untillDate.DayOfWeek.ToString() + " "  + untillDate.Date.ToShortDateString();
        }

        public void Refresh()
        {
            LoadModelAndUpdateView();
        }

        public string GetDynamicDescription()
        {
            return _dynamicDescription;
        }
    }
}