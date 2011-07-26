using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Working Days Left", StaticDescription = "Actual working days left")]
    public class WorkingDaysLeftWidget : RelativeLayout, IWidget
    {
        private WorkingDaysLeft model;

        private string dynamicDescription;
        private string DynamicDescription
        {
            get { return dynamicDescription; }
            set
            {
                if (value != dynamicDescription)
                {
                    dynamicDescription = value;
                    if (DescriptionChanged != null) DescriptionChanged(this, null);
                }
            }
        }

        public event EventHandler DescriptionChanged;

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
            model.Load(() => ((Activity)Context).RunOnUiThread(Redraw));
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
            if (model.LoadError)
            {
                DynamicDescription = "Failed to load project info from server";
                daysView.Visibility = ViewStates.Invisible;
                textView.Visibility = ViewStates.Invisible;
                return;
            }
            daysView.Visibility = ViewStates.Visible;
            textView.Visibility = ViewStates.Visible;
            
            if (model.IsOnOvertime)
                DynamicDescription = "You should have been finished by " + untillDate.DayOfWeek.ToString() + " " +
                                      untillDate.Date.ToShortDateString();
            else
                DynamicDescription = "Working days left untill " + untillDate.DayOfWeek.ToString() + " " + untillDate.Date.ToShortDateString();
        }

        public string GetDynamicDescription()
        {
            return DynamicDescription;
        }

        public void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }
    }
}