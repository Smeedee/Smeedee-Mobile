using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Working Days Left", StaticDescription = "Actual working days left")]
    public class WorkingDaysLeftWidget : RelativeLayout, IWidget
    {
        private WorkingDaysLeft model;
        private DateTime _lastRefreshTime;

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
            model.Load(() => ((Activity) Context).RunOnUiThread(() =>
                {
                    Redraw();
                    OnDescriptionChanged(new EventArgs());
                }));
            _lastRefreshTime = DateTime.Now;
        }
      

        public DateTime LastRefreshTime()
        {
            return _lastRefreshTime;
        }

        public void Redraw()
        {
            var daysView = FindViewById<TextView>(Resource.Id.WorkingDaysLeftNumber);
            var textView = FindViewById<TextView>(Resource.Id.WorkingDaysLeftText);

            daysView.Text = model.DaysLeft.ToString();
            textView.Text = model.DaysLeftText;
            
            if (model.LoadError)
            {
                daysView.Visibility = ViewStates.Invisible;
                textView.Visibility = ViewStates.Invisible;
                return;
            }
            daysView.Visibility = ViewStates.Visible;
            textView.Visibility = ViewStates.Visible;
        }

        public string GetDynamicDescription()
        {
            return model.DynamicDescription;
        }

        public void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }
    }
}