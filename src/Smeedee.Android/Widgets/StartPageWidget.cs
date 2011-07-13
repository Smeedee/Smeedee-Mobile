using System;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("")]
    public class StartPageWidget : RelativeLayout, IWidget
    {
        private string _dynamicDescription;

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
        }

        public string GetDynamicDescription()
        {
            return _dynamicDescription;
        }
    }
}