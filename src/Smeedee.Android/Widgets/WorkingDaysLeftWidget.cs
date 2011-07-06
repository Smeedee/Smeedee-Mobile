using System;

using Android.Content;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    public class WorkingDaysLeftWidget : RelativeLayout, IWidget
    {
        private WorkingDaysLeft model;

        public WorkingDaysLeftWidget(Context context) : base(context)
        {
            InitializeView();

            model = new WorkingDaysLeft();
            model.Load(UpdateView);
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
            var textView = FindViewById<TextView>(Resource.Id.WorkingDaysLeftNumber);
            var days = model.DaysLeft.ToString();
            textView.SetText(days, TextView.BufferType.Normal);
        }
    }
}