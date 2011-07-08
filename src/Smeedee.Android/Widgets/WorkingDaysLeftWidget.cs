using System;

using Android.Content;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Working Days Left", Resource.Drawable.icon_workingdaysleft, IsEnabled = true)]
    public class WorkingDaysLeftWidget : RelativeLayout/*, IWidget*/
    {
        private readonly IModelService<WorkingDaysLeft> modelService =
            SmeedeeApp.Instance.ServiceLocator.Get<IModelService<WorkingDaysLeft>>();
        
        private WorkingDaysLeft _model;

        public WorkingDaysLeftWidget(Context context) : base(context)
        {
            InflateView();
            InitializeModelAndUpdateView();
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

        private void InitializeModelAndUpdateView()
        {
            // TODO: This queued in a thread, modelService is synchronous
            _model = modelService.GetSingle();
            UpdateView();
        }

        private void UpdateView()
        {
            var daysView = FindViewById<TextView>(Resource.Id.WorkingDaysLeftNumber);
            var textView = FindViewById<TextView>(Resource.Id.WorkingDaysLeftText);
            
            var days = _model.DaysLeft.ToString();
            var text = _model.DaysLeftText;

            daysView.SetText(days, TextView.BufferType.Normal);
            textView.SetText(text, TextView.BufferType.Normal);
        }
    }
}