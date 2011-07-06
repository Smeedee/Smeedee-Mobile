using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Services;

namespace Smeedee.Android.Views
{
    public class LoginView : LinearLayout, IWidget
    {
        public LoginView(Context context)
            : base(context)
        {
            Initialize();
        }

        public LoginView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            inflater.Inflate(Resource.Layout.LoginViewLayout, this);
        }
    }
}