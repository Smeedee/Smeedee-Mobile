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
        private WorkingDaysLeft model;
        private string _dynamicDescription;

        public WorkingDaysLeftWidget(Context context) : base(context)
        {
            InflateView();
            model = new WorkingDaysLeft();
            Refresh();
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
        
        public void Refresh()
        {
            model.Load(Redraw);
        }

        public void Redraw()
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
                _dynamicDescription = "Working days left untill " + untillDate.DayOfWeek.ToString() + " " + untillDate.Date.ToShortDateString();

        }

        public string GetDynamicDescription()
        {
            return _dynamicDescription;
        }

        public event EventHandler DescriptionChanged;
        public void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }
    }
}