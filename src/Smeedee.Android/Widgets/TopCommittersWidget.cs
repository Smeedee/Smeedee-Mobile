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

namespace Smeedee.Android.Widgets
{
    [Widget("Top Committers", Resource.Drawable.Icon, IsEnabled = true)]
    public class TopCommittersWidget : RelativeLayout , IWidget
    {
        public TopCommittersWidget(Context context) : base(context)
        {
            InitializeView();
        }
        
        private void InitializeView()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null)
            {
                inflater.Inflate(Resource.Layout.TopCommittersWidget, this);
            }
            else
            {
                throw new Exception("Unable to inflate view on Working days left widget");
            }
        }
    }
}