using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android.Widgets
{
    public class WorkingDaysLeftWidget : RelativeLayout, IWidget
    {
        private WorkingDaysLeft model;

        public WorkingDaysLeftWidget(Context context) : base(context)
        {
            Initialize();

            model = new WorkingDaysLeft();
            model.Load(UpdateView);
        }

        private void Initialize()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            inflater.Inflate(Resource.Layout.WorkingDaysLeftWidget, this);
        }

        private void UpdateView()
        {
            var textView = FindViewById<TextView>(Resource.Id.WorkingDaysLeftNumber);
            var days = model.DaysLeft.ToString();
            textView.SetText(days, TextView.BufferType.Normal);
        }

    }
}