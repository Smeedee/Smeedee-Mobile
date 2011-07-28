using System;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Smeedee")]
    public class StartPageWidget : RelativeLayout, IWidget
    {
        private const string DynamicDescription = "Flip left and right to see your data";

        public event EventHandler DescriptionChanged;
        private DateTime _lastRefreshTime;

        public StartPageWidget(Context context) :
            base(context)
        {
            Initialize();
        }

        public StartPageWidget(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null) 
                inflater.Inflate(Resource.Layout.StartPageWidget, this);
                else
            {
                throw new Exception("Unable to inflate view on Start Page Widget");
            }
        }

        public void Refresh()
        {
            _lastRefreshTime = DateTime.Now;
        }

        public DateTime LastRefreshTime()
        {
            return _lastRefreshTime;
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