using System;

using Android.Content;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Working Days Left", "@drawable/Icon", IsEnabled = true)]
    public class WorkingDaysLeftWidget : RelativeLayout, IWidget
    {
        private readonly WorkingDaysLeft _model;

        public WorkingDaysLeftWidget(Context context) : base(context)
        {
            InitializeView();

            _model = new WorkingDaysLeft();
            _model.Load(UpdateView);
        }

        private void InitializeView()
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